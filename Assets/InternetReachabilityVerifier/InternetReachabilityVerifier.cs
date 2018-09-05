// InternetReachabilityVerifier
//
// Helper class for verifying actual connectivity to Internet.
// Implements various "captive portal detection" methods using Unity.
//
// Copyright 2014-2018 Jetro Lauha (Strobotnik Ltd)
// http://strobotnik.com
// http://jet.ro
//
// $Revision: 1309 $
//
// File version history:
// 2014-06-18, 1.0.0 - Initial version
// 2014-07-22, 1.0.1 - Added Apple2 method (now default for Apple platforms).
// 2014-09-15, 1.0.2 - Refined customMethodExpectedData check to support
//                     expected empty result. Added option to append use a
//                     "cache buster" query string when using custom method.
//                     Added getTimeWithoutInternetConnection().
// 2015-03-26, 1.0.3 - Support for Unity 5. Made DontDestroyOnLoad optional.
//                     Also start/stop netActivity in OnEnable/OnDisable.
// 2016-08-27, 1.1.0 - Changed internal coroutine to wait using realtime
//                     instead of pauseable normal time. Also the 
//                     getTimeWithoutInternetConnection is now realtime-based.
//                     Added forceReverification method which sets status
//                     to pending verification, which is also now internally
//                     noticed immediately. Exposed the time value settings to
//                     inspector. Added AppleHTTPS method (due to ATS) and
//                     waitForNetVerifiedStatus convenience helper coroutine.
//                     Fixed regression bug of running verification twice.
// 2017-04-06, 1.1.1 - Added tooltips.
// 2017-04-12, 1.1.2 - Hotfix for Apple methods.
// 2018-03-06, 1.1.3 - Fixed deprecation warning with latest Unity versions.
//                     Made responseHeaders related code bit more robust.

#define DEBUG_ERRORS
#if UNITY_EDITOR
//#define DEBUG_LOGS
#define DEBUG_WARNINGS
#endif

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class InternetReachabilityVerifier : MonoBehaviour
{
    //! Method to detect if network only reaches a "captive portal".
    /*! DefaultByPlatform picks the "platform provider" method,
     * e.g. Android->Google204, iOS->AppleHTTPS, Windows->MicrosoftNCSI.
     */
    public enum CaptivePortalDetectionMethod
    {
        DefaultByPlatform = 0, //!< Use "native" method for current platform.
        Google204, //!< Google's "HTTP result 204" method used by e.g. Android.
        GoogleBlank, //!< Google's fallback method, a blank page from google.com.
        MicrosoftNCSI, //!< Microsoft Network Connectivity Status Indicator check.
        Apple, //!< Fetch html page from apple.com with word "Success".
        Ubuntu, //!< Ubuntu connectivity check, returns Lorem ipsum.
        Custom, //!< Test using your self-hosted server.
        Apple2, //!< Like Apple method but use captive.apple.com and random path.
        AppleHTTPS, //!< Same as Apple but fetch using HTTPS.
    };

    public CaptivePortalDetectionMethod captivePortalDetectionMethod = CaptivePortalDetectionMethod.DefaultByPlatform;
    #if !UNITY_3_5 && !UNITY_4_0 && !UNITY_4_1 && !UNITY_4_2 && !UNITY_4_3
    [Tooltip("Self-hosted URL for using CaptivePortalDetectionMethod.Custom. For example: https://example.com/IRV.txt")]
    #endif
    public string customMethodURL = "";
    #if !UNITY_3_5 && !UNITY_4_0 && !UNITY_4_1 && !UNITY_4_2 && !UNITY_4_3
    [Tooltip("Data expected from the custom self-hosted URL. By default the data returned by the custom url is expected to start with contents of this string. Alternatively you can set the customMethodVerifierDelegate (see example code), in which case this string will be passed to the delegate.")]
    #endif
    public string customMethodExpectedData = "OK";
    #if !UNITY_3_5 && !UNITY_4_0 && !UNITY_4_1 && !UNITY_4_2 && !UNITY_4_3
    [Tooltip("Makes the IRV object not be destroyed automatically when loading a new scene.")]
    #endif
    public bool dontDestroyOnLoad = true;
    #if !UNITY_3_5 && !UNITY_4_0 && !UNITY_4_1 && !UNITY_4_2 && !UNITY_4_3
    [Tooltip("When enabled, custom method URL is appended with a query string containing a random number.\nExample of what such a query string may look like: ?z=13371337")]
    #endif
    public bool customMethodWithCacheBuster = true;
    #if !UNITY_3_5 && !UNITY_4_0 && !UNITY_4_1 && !UNITY_4_2 && !UNITY_4_3
    [Tooltip("Default time in seconds to wait until trying to verify network connectivity again.\nSuggested minimum: 1 second.")]
    #endif
    public float defaultCheckPeriod = 4.0f;
    #if !UNITY_3_5 && !UNITY_4_0 && !UNITY_4_1 && !UNITY_4_2 && !UNITY_4_3
    [Tooltip("Time in seconds to wait before retrying, after last verification attempt resulted in an error.\nSuggested minimum: 3 seconds.")]
    #endif
    public float errorRetryDelay = 15.0f;
    #if !UNITY_3_5 && !UNITY_4_0 && !UNITY_4_1 && !UNITY_4_2 && !UNITY_4_3
    [Tooltip("Time in seconds to wait after detecting a captive portal (WiFi login screen).\nSuggested minimum: 2 seconds.")]
    #endif
    public float mismatchRetryDelay = 7.0f;


    public enum Status
    {
        Offline,             // no network connectivity / no internet access
        PendingVerification, // have network connectivity, internet access is being verified
        Error,               // error trying to verify internet access, will retry shortly
        Mismatch,            // captive portal detected (e.g. wifi login screen), will retry shortly
        NetVerified          // internet access is verified and functional
    }

    public delegate void StatusChangedDelegate(Status newStatus);
    public event StatusChangedDelegate statusChangedDelegate = null;

    public delegate bool CustomMethodVerifierDelegate(WWW www, string customMethodExpectedData);
    public CustomMethodVerifierDelegate customMethodVerifierDelegate = null;

    float noInternetStartTime = 0;

    Status _status = Status.Offline;
    public Status status
    {
        get
        {
            return _status;
        }
        set
        {
            Status prevStatus = _status;
            _status = value;
            if (prevStatus == Status.NetVerified && _status != Status.NetVerified)
                noInternetStartTime = Time.realtimeSinceStartup;
            if (statusChangedDelegate != null)
                statusChangedDelegate(value);
        }
    }

    string _lastError = "";
    public string lastError
    {
        get
        {
            return _lastError;
        }
        set
        {
            _lastError = value;
        }
    }

    private static InternetReachabilityVerifier _instance = null;
    public static InternetReachabilityVerifier Instance { get { return _instance; } }


    static RuntimePlatform[] methodGoogle204Supported = new RuntimePlatform[]
    {
        RuntimePlatform.WindowsPlayer,
        RuntimePlatform.WindowsEditor,
        RuntimePlatform.Android,
        RuntimePlatform.LinuxPlayer,
        RuntimePlatform.OSXPlayer,
        RuntimePlatform.OSXEditor,
    };

    const CaptivePortalDetectionMethod fallbackMethodIfNoDefaultByPlatform = CaptivePortalDetectionMethod.MicrosoftNCSI;

    bool netActivityRunning = false;

    string apple2MethodURL = "";


    //! Returns how long app has been without internet connection (time in seconds).
    /*! Returns 0 when online (internet connection is supposedly available).
     */
    public float getTimeWithoutInternetConnection()
    {
        if (status == Status.NetVerified)
            return 0; // we're online
        else
            return Time.realtimeSinceStartup - noInternetStartTime; // time without internet in seconds
    }

#if UNITY_EDITOR
    [ContextMenu("Debug.Log Internet Reachability Verifier info")]
    void debugLogInfo()
    {
        Debug.Log("IRV status: " + status + 
                  ", time without internet connection: " +
                  getTimeWithoutInternetConnection() + " seconds");
    }
#endif


    //! Helper coroutine for waiting until status becomes NetVerified if it isn't already.
    /*! If status isn't already PendingVerification, will also force reverification first.
     */
    public IEnumerator waitForNetVerifiedStatus()
    {
        if (status != Status.NetVerified)
            forceReverification();
        while (status != Status.NetVerified)
            yield return null;
    }

    //! Sets net activity time wait periods.
    /*! There are reasonable defaults, so there is no need to call this
     *  at all unless you want to change the times.
     */
    public void setNetActivityTimes(float defaultCheckPeriodSeconds,
                                    float errorRetryDelaySeconds,
                                    float mismatchRetryDelaySeconds)
    {
#       if DEBUG_WARNINGS
        if (defaultCheckPeriodSeconds < 1.0f)
            Debug.LogWarning("IRV - custom defaultCheckPeriodSeconds is set to a very low value: " + defaultCheckPeriodSeconds, this);
        if (errorRetryDelaySeconds < 3.0f)
            Debug.LogWarning("IRV - custom errorRetryDelaySeconds is set to a very low value: " + errorRetryDelaySeconds, this);
        if (mismatchRetryDelaySeconds < 2.0f)
            Debug.LogWarning("IRV - custom mismatchRetryDelaySeconds is set to a very low value: " + mismatchRetryDelaySeconds, this);
#       endif
        defaultCheckPeriod = defaultCheckPeriodSeconds;
        errorRetryDelay = errorRetryDelaySeconds;
        mismatchRetryDelay = mismatchRetryDelaySeconds;
    }

    //! Requests that the internet access is verified again.
    /*! You should call this after your own networking calls start to return
     *  errors which indicate effective loss of network connectivity.
     * \note equivalent to forcing the status to PendingVerification.
     */
    public void forceReverification()
    {
        status = Status.PendingVerification;
    }


    string getCaptivePortalDetectionURL(CaptivePortalDetectionMethod cpdm)
    {
        if (cpdm == CaptivePortalDetectionMethod.Custom)
        {
            string url = customMethodURL;
            if (customMethodWithCacheBuster)
                url += "?z=" + (Random.Range(0, 0x7fffffff) ^ 0x13377AA7);
            return url;
        }
        else if (cpdm == CaptivePortalDetectionMethod.Google204)
            return "http://clients3.google.com/generate_204";
        else if (cpdm == CaptivePortalDetectionMethod.MicrosoftNCSI)
            return "http://www.msftncsi.com/ncsi.txt";
        else if (cpdm == CaptivePortalDetectionMethod.GoogleBlank)
            return "http://www.google.com/blank.html";
        else if (cpdm == CaptivePortalDetectionMethod.Apple)
            return "http://www.apple.com/library/test/success.html";
        else if (cpdm == CaptivePortalDetectionMethod.Ubuntu)
            return "http://start.ubuntu.com/connectivity-check";
        else if (cpdm == CaptivePortalDetectionMethod.Apple2)
        {
            if (apple2MethodURL.Length == 0)
            {
                apple2MethodURL = "http://captive.apple.com/";
                char[] path = new char[17];
                for (int a = 0; a < 17; ++a)
                    path[a] = (char)((int)'a' + Random.Range(0, 'z' - 'a' + 1));
                path[8] = '/';
                apple2MethodURL += new string(path);
#               if DEBUG_LOGS
                Debug.Log("IRV using apple2MethodURL: " + apple2MethodURL);
#               endif
            }
            return apple2MethodURL;
        }
        else if (cpdm == CaptivePortalDetectionMethod.AppleHTTPS)
        {
            return "https://www.apple.com/library/test/success.html";
        }
        return "";
    }

    bool checkCaptivePortalDetectionResult(CaptivePortalDetectionMethod cpdm, WWW www)
    {
        if (www == null)
        {
#           if DEBUG_WARNINGS
            Debug.LogWarning("IRV checkCaptivePortalDetectionResult - www is null!", this);
#           endif
            return false; // error
        }
#       if DEBUG_LOGS
        Debug.Log("IRV checkCaptivePortalDetectionResult cpdm:" + cpdm + ", www size:" + www.size + ", data:" + www.text, this);
        if (www.responseHeaders != null && www.responseHeaders.Keys != null &&
            www.responseHeaders.Keys.Count > 0)
        {
            string hdrnfo = "IRV - " + www.responseHeaders.Keys.Count + " response headers:\n";
            foreach (string key in www.responseHeaders.Keys)
                hdrnfo += key + ": " + www.responseHeaders[key] + "\n";
            Debug.Log(hdrnfo, this);
        }
#       endif

        if (www.error != null && www.error.Length > 0)
            return false; // www ended up in error, can't be success

        switch (cpdm)
        {
            case CaptivePortalDetectionMethod.Custom:
#               if DEBUG_WARNINGS
                if (www.responseHeaders != null &&
                    www.responseHeaders.ContainsKey("CACHE-CONTROL"))
                {
                    Debug.LogWarning("IRV - Cache-Control header contents: " + www.responseHeaders["CACHE-CONTROL"], this);
                    Debug.LogWarning("IRV - Warning, custom www response contains Cache-Control header - you should verify its contents. Recommendation is to have no caching or very short max-age.", this);
                }
#               endif
                if (customMethodVerifierDelegate != null)
                    return customMethodVerifierDelegate(www, customMethodExpectedData);
                else if ((customMethodExpectedData.Length > 0 &&
                          www.text != null &&
                          www.text.StartsWith(customMethodExpectedData)) ||
                         (customMethodExpectedData.Length == 0 &&
                          (www.bytes == null || www.bytes.Length == 0)))
                    return true;
                break;

            case CaptivePortalDetectionMethod.Google204:
                Dictionary<string,string> responseHeaders = www.responseHeaders;
                if (responseHeaders != null && responseHeaders.Keys != null && responseHeaders.Keys.Count > 0)
                {
                    string httpStatus = "";
                    if (responseHeaders.ContainsKey("STATUS")) // some platforms
                        httpStatus = responseHeaders["STATUS"];
                    else if (responseHeaders.ContainsKey("NULL")) // on android
                        httpStatus = responseHeaders["NULL"];
                    if (httpStatus.Length > 0)
                    {
                        if (httpStatus.IndexOf("204 No Content") >= 0)
                            return true;
                    }
                }
                else
                {
#                   if UNITY_5_3_OR_NEWER
                    if (www.bytesDownloaded == 0)
#                   else // 3.x, 4.x, 5.0-5.2
                    if (www.size == 0)
#                   endif
                    {
                        // Some versions of Unity WWW class implementation don't always give
                        // response headers. In that case (or if forcibly using Google204
                        // method in other platforms), we have to fall back to check the
                        // data size, same way how the GoogleBlank check works.
                        return true;
                    }
                }
                break;

            case CaptivePortalDetectionMethod.GoogleBlank:
#               if UNITY_5_3_OR_NEWER
                if (www.bytesDownloaded == 0)
#               else // 3.x, 4.x, 5.0-5.2
                if (www.size == 0)
#               endif
                {
                    return true;
                }
                break;

            case CaptivePortalDetectionMethod.MicrosoftNCSI:
                if (www.text.StartsWith("Microsoft NCSI"))
                    return true;
                break;

            case CaptivePortalDetectionMethod.Apple:
            case CaptivePortalDetectionMethod.Apple2:
            case CaptivePortalDetectionMethod.AppleHTTPS:
                // returns a short html doc, do a semi-soft check for it
                string lowerText = www.text.ToLower();
                int bodySuccessPos = lowerText.IndexOf("<body>success</body>");
                int titleSuccessPos = lowerText.IndexOf("<title>success</title>");
                if ((bodySuccessPos >= 0 && bodySuccessPos < 500) ||
                    (titleSuccessPos >= 0 && titleSuccessPos < 500))
                    return true;
                break;

            case CaptivePortalDetectionMethod.Ubuntu:
                // returns a whole html doc with lorem ipsum text,
                // let's use a smaller check for it (start of body)
                if (www.text.IndexOf("Lorem ipsum dolor sit amet") == 109)
                    return true;
                break;
        }

        return false;
    }


    private float _yieldWaitStart = 0;

    private bool internal_yieldWait(float seconds)
    {
        if (_yieldWaitStart == 0)
            _yieldWaitStart = Time.realtimeSinceStartup;
        bool yieldWait = (Time.realtimeSinceStartup - _yieldWaitStart) < seconds;
        if (!yieldWait)
            _yieldWaitStart = 0;
        return yieldWait;
    }


    IEnumerator netActivity()
    {
        netActivityRunning = true;

        NetworkReachability prevUnityReachability = Application.internetReachability;

        if (Application.internetReachability != NetworkReachability.NotReachable)
            status = Status.PendingVerification;
        else
            status = Status.Offline;
        noInternetStartTime = Time.realtimeSinceStartup;

        while (netActivityRunning)
        {
#           if DEBUG_LOGS
            Debug.Log("IRV netActivity cycle, status: " + status, this);
#           endif

            if (status == Status.Error)
            {
                while (internal_yieldWait(errorRetryDelay) && status != Status.PendingVerification) yield return null;
                status = Status.PendingVerification;
            }
            else if (status == Status.Mismatch)
            {
                while (internal_yieldWait(mismatchRetryDelay) && status != Status.PendingVerification) yield return null;
                status = Status.PendingVerification;
            }

            NetworkReachability unityReachability = Application.internetReachability;
            if (prevUnityReachability != unityReachability)
            {
#               if DEBUG_LOGS
                Debug.Log("IRV unity reachability changed: " + unityReachability, this);
#               endif
                if (unityReachability != NetworkReachability.NotReachable)
                {
                    status = Status.PendingVerification;
                }
                else if (unityReachability == NetworkReachability.NotReachable)
                {
                    status = Status.Offline;
                }
                prevUnityReachability = Application.internetReachability;
            }

            if (status == Status.PendingVerification)
            {
                verifyCaptivePortalDetectionMethod();
                CaptivePortalDetectionMethod cpdm = this.captivePortalDetectionMethod;
                string url = getCaptivePortalDetectionURL(cpdm);
#               if DEBUG_LOGS
                Debug.Log("IRV - trying to verify internet access with method " + cpdm + " and url:" + url, this);
#               endif
                WWW www = new WWW(url);
                yield return www;
                if (www.error != null && www.error.Length > 0)
                {
#                   if DEBUG_LOGS
                    Debug.Log("IRV www error: " + www.error, this);
#                   endif
                    lastError = www.error;
#                   if DEBUG_WARNINGS
                    if (lastError.Contains("no crossdomain.xml"))
                    {
                        Debug.LogWarning("IRV www error: " + www.error, this);
                        Debug.LogWarning("See http://docs.unity3d.com/462/Documentation/Manual/SecuritySandbox.html", this);
                        Debug.LogWarning("You should also check WWW Security Emulation Host URL of Unity Editor in Edit->Project Settings->Editor", this);
                    }
#                   endif
                    status = Status.Error;
                    continue;
                }
                else // or no www error, verify result:
                {
                    bool success = checkCaptivePortalDetectionResult(cpdm, www);
                    if (success)
                    {
#                       if DEBUG_LOGS
                        Debug.Log("IRV net access verification success", this);
#                       endif
                        status = Status.NetVerified; // success
                    }
                    else
                    {
#                       if DEBUG_LOGS
                        Debug.Log("IRV net verification mismatch (network login screen?)", this);
                        //Debug.Log(www.data, this); // for debug peeking at data
#                       endif
                        status = Status.Mismatch;
                        continue;
                    }
                } // no www error, verify result
            } // netAccessStatus == NetAccessStatus.PendingVerification

            while (internal_yieldWait(defaultCheckPeriod) && status != Status.PendingVerification) yield return null;
        } // while true

        netActivityRunning = false;
        status = Status.PendingVerification;

    } // netActivity


    void Awake()
    {
        // prevent additional objects being created on level reloads
        if (_instance)
        {
            DestroyImmediate(this.gameObject);
            return;
        }
        _instance = this;
        if (dontDestroyOnLoad)
            DontDestroyOnLoad(gameObject);
    }

    void verifyCaptivePortalDetectionMethod()
    {
        // if we're using DefaultByPlatform, figure out what's platform's
        // "native" method (platform-vendor-provided way), and switch to it
        // (or use fallback if there is no such thing)

        if (captivePortalDetectionMethod == CaptivePortalDetectionMethod.DefaultByPlatform)
        {
#           if UNITY_STANDALONE_OSX || UNITY_DASHBOARD_WIDGET || UNITY_IPHONE || UNITY_IOS || UNITY_TVOS
            captivePortalDetectionMethod = CaptivePortalDetectionMethod.AppleHTTPS;
#           elif UNITY_STANDALONE_WIN
            captivePortalDetectionMethod = CaptivePortalDetectionMethod.MicrosoftNCSI;
#           elif UNITY_WEBPLAYER || UNITY_FLASH || UNITY_NACL || UNITY_WEBGL
            captivePortalDetectionMethod = CaptivePortalDetectionMethod.Custom;
#           elif UNITY_ANDROID
            captivePortalDetectionMethod = CaptivePortalDetectionMethod.Google204;
#           elif UNITY_STANDALONE_LINUX
            captivePortalDetectionMethod = CaptivePortalDetectionMethod.Ubuntu;
#           elif UNITY_METRO || UNITY_WP8 || UNITY_WP_8_1 || UNITY_WSA
            captivePortalDetectionMethod = CaptivePortalDetectionMethod.MicrosoftNCSI;
#           elif UNITY_BLACKBERRY
#           if DEBUG_WARNINGS
            Debug.LogWarning("IRV - " + Application.platform + " platform isn't supported/tested (however, it probably works - trying to use default fallback)", this);
#           endif
#           elif UNITY_WII || UNITY_PS3 || UNITY_PS4 || UNITY_XBOX360 || UNITY_XBOXONE || UNITY_TIZEN || UNITY_SAMSUNGTV
#           if DEBUG_WARNINGS
            Debug.LogWarning("IRV - " + Application.platform + " platform isn't supported/tested", this);
#           endif
#           endif

            //cpdmURLs[(int)CaptivePortalDetectionMethod.DefaultByPlatform] = cpdmURLs[(int)cpdm];
            if (captivePortalDetectionMethod == CaptivePortalDetectionMethod.DefaultByPlatform)
            {
                // there's no "native" one, use fallback
                captivePortalDetectionMethod = fallbackMethodIfNoDefaultByPlatform;
            }
#           if DEBUG_LOGS
            Debug.Log("IRV - default by platform selected, using " + captivePortalDetectionMethod + " method", this);
#           endif
        }

#if UNITY_WEBPLAYER || UNITY_FLASH || UNITY_NACL || UNITY_WEBGL
        if (captivePortalDetectionMethod != CaptivePortalDetectionMethod.Custom)
        {
            if (Application.platform == RuntimePlatform.NaCl ||
                Application.platform == RuntimePlatform.FlashPlayer)
            {
#               if DEBUG_WARNINGS
                Debug.LogWarning("IRV - " + Application.platform + " platform isn't supported/tested (However, using the Custom method may work)", this);
#               endif
            }
#           if DEBUG_WARNINGS
            Debug.LogWarning("IRV - Web-based platform selected - forcing custom method! (" + Application.platform + ")", this);
#           endif
            captivePortalDetectionMethod = CaptivePortalDetectionMethod.Custom;
        }
#endif // webplayer|flash|nacl|webgl

        if (captivePortalDetectionMethod == CaptivePortalDetectionMethod.Google204)
        {
            if (System.Array.IndexOf(methodGoogle204Supported, Application.platform) < 0)
            {
                // WWW impl. of current runtime can't check http status code
#               if DEBUG_WARNINGS
                Debug.LogWarning("IRV - Can't use Google204 method on " + Application.platform + ", using GoogleBlank as fallback", this);
#               endif
                captivePortalDetectionMethod = CaptivePortalDetectionMethod.GoogleBlank;
            }
        }

        if (captivePortalDetectionMethod == CaptivePortalDetectionMethod.Custom &&
            customMethodURL.Length == 0)
        {
#           if DEBUG_ERRORS
            Debug.LogError("IRV - Custom method is selected but URL is empty, cannot start! (disabling component)", this);
#           endif
            this.enabled = false;
            if (netActivityRunning)
                Stop();
            return; // bail out - no URL to use!
        }
    }

    void Start()
    {
        verifyCaptivePortalDetectionMethod();

#       if DEBUG_WARNINGS
#       if !UNITY_3_5 && !UNITY_4_0 && !UNITY_4_1 && !UNITY_4_2
        // Unity 4.3+
        bool notSupportedWarning = false;
        if (Application.platform == RuntimePlatform.TizenPlayer)
            notSupportedWarning = true;
#       if !UNITY_4_3
        // Unity 4.5+:
        if (Application.platform == RuntimePlatform.PSP2 ||
            Application.platform == RuntimePlatform.PS4 ||
            Application.platform == RuntimePlatform.XboxOne ||
            Application.platform == RuntimePlatform.SamsungTVPlayer)
            notSupportedWarning = true;
#       endif // 4.5+

#       if UNITY_4_5 || UNITY_4_6
        // Strictly Unity 4.5 or 4.6:
        if (Application.platform == RuntimePlatform.PSMPlayer)
            notSupportedWarning = true;
#       endif // 4.5 or 4.6

#       if UNITY_5_0
        // Strictly Unity 5.0:
        if (Application.platform == RuntimePlatform.PSM)
            notSupportedWarning = true;
#       endif // 5.0

#       if UNITY_5_2 || UNITY_5_3 || UNITY_5_3_OR_NEWER
        if (Application.platform == RuntimePlatform.WiiU)
            notSupportedWarning = true;
#       endif

#       if UNITY_5_5_OR_NEWER
        if (Application.platform == RuntimePlatform.Switch)
            notSupportedWarning = true;
#       endif

        if (notSupportedWarning)
            Debug.LogWarning("IRV - " + Application.platform + " platform isn't supported/tested", this);
#       endif // unity 4.3+
#       endif // debug_warnings

        if (!netActivityRunning)
            StartCoroutine("netActivity");
    }

    void OnDisable()
    {
#       if DEBUG_LOGS
        Debug.Log("IRV - OnDisable", this);
#       endif
        Stop();
    }

    void OnEnable()
    {
#       if DEBUG_LOGS
        Debug.Log("IRV - OnEnable", this);
#       endif
        Start();
    }

    public void Stop()
    {
        StopCoroutine("netActivity");
        netActivityRunning = false;
    }

} // InternetReachabilityVerifier
