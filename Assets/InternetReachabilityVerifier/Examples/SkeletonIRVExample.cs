// Skeleton example for InternetReachabilityVerifier.
//
// Copyright 2014-2016 Jetro Lauha (Strobotnik Ltd)
// http://strobotnik.com
// http://jet.ro
//
// $Revision: 1046 $
//
// File version history:
// 2014-06-18, 1.0.0 - Initial version
// 2016-08-27, 1.1.0 - Added waitForNetwork method (example of using
//                     waitForNetVerifiedStatus). Other minor changes.

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(InternetReachabilityVerifier))]
public class SkeletonIRVExample : MonoBehaviour
{
    InternetReachabilityVerifier internetReachabilityVerifier;

    // Returns true when there is verified internet access.
    bool isNetVerified()
    {
        return internetReachabilityVerifier.status == InternetReachabilityVerifier.Status.NetVerified;
    }

    // Requests that the internet access is verified again.
    /* This is done by simply force "pending verification" status,
     * InternetReachabilityVerifier has a coroutine which will notice this.
     * (You should do this if you know some other way that
     *  network connectivity has been lost or something.)
     */
    void forceReverification()
    {
        internetReachabilityVerifier.forceReverification();
    }


    // Example of how to listen for all network status changes. (recommended way)
    // Note: this delegate is set up in Start() below.
    void netStatusChanged(InternetReachabilityVerifier.Status newStatus)
    {
        Debug.Log("netStatusChanged: new InternetReachabilityVerifier.Status = " + newStatus);
    }


    // Additional optional way - coroutine which keeps waiting until network is available.
    IEnumerator waitForNetwork()
    {
        yield return new WaitForEndOfFrame(); // start waiting after initialization has finished
        yield return StartCoroutine(internetReachabilityVerifier.waitForNetVerifiedStatus());
        Debug.Log("waitForNetwork coroutine succeeded and stopped.");
    }


    void Start()
    {
        // Setup status change listening with a delegate. (recommended way)
        internetReachabilityVerifier = GetComponent<InternetReachabilityVerifier>();
        internetReachabilityVerifier.statusChangedDelegate += netStatusChanged;

        // Set up additional optional way - the coroutine waits until network is availble.
        StartCoroutine(waitForNetwork());

        // Note: See the CustomIRVExample for more info about how to
        //       verify Internet access using your self-hosted URL.
    }
}
