//----------------------------------------------
// Flip Web Apps: Beautiful Transitions
// Copyright © 2016 Flip Web Apps / Mark Hewitt
//
// Please direct any bugs/comments/suggestions to http://www.flipwebapps.com
// 
// The copyright owner grants to the end user a non-exclusive, worldwide, and perpetual license to this Asset
// to integrate only as incorporated and embedded components of electronic games and interactive media and 
// distribute such electronic game and interactive media. End user may modify Assets. End user may otherwise 
// not reproduce, distribute, sublicense, rent, lease or lend the Assets. It is emphasized that the end 
// user shall not be entitled to distribute or transfer in any way (including, without, limitation by way of 
// sublicense) the Assets in any other way than as integrated components of electronic games and interactive media. 

// The above copyright notice and this permission notice must not be removed from any files.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//----------------------------------------------

using UnityEngine;
using UnityEngine.UI;

namespace FlipWebApps.BeautifulTransitions.Scripts.Transitions.TransitionSteps
{
    /// <summary>
    /// Global scheduler and singleton for behind the scenes management of transitions
    /// </summary>
    public class TransitionController : MonoBehaviour
    {
        #region Screen Fade Properties

        /// <summary>
        /// Gameobject to use for screen fade
        /// </summary>
        public UnityEngine.GameObject TransitionScreenFadeGameObject
        {
            get {
                if (_transitionScreenFadeGameObject == null)
                    SetupScreenFadeComponents();
                return _transitionScreenFadeGameObject;
            }
            private set { _transitionScreenFadeGameObject = value; }
        }
        UnityEngine.GameObject _transitionScreenFadeGameObject;

        /// <summary>
        /// Raw Image to use for screen fade
        /// </summary>
        public RawImage TransitionScreenFadeRawImage
        {
            get {
                if (_transitionScreenFadeRawImage == null)
                    SetupScreenFadeComponents();
                return _transitionScreenFadeRawImage;
            }
            set { _transitionScreenFadeRawImage = value; }
        }
        RawImage _transitionScreenFadeRawImage;

        #endregion Screen Fade Properties

        #region Screen Wipe Properties

        /// <summary>
        /// Gameobject to use for screen wipe
        /// </summary>
        public UnityEngine.GameObject TransitionScreenWipeGameObject
        {
            get
            {
                if (_transitionScreenWipeGameObject == null)
                    SetupScreenWipeComponents();
                return _transitionScreenWipeGameObject;
            }
            private set { _transitionScreenWipeGameObject = value; }
        }
        UnityEngine.GameObject _transitionScreenWipeGameObject;

        /// <summary>
        /// Raw Image to use for screen wipe
        /// </summary>
        public RawImage TransitionScreenWipeRawImage
        {
            get
            {
                if (_transitionScreenWipeRawImage == null)
                    SetupScreenWipeComponents();
                return _transitionScreenWipeRawImage;
            }
            set { _transitionScreenWipeRawImage = value; }
        }
        RawImage _transitionScreenWipeRawImage;

        public Material TransitionScreenWipeMaterial
        {
            get
            {
                if (_transitionScreenWipeMaterial == null)
                    SetupScreenWipeComponents();
                return _transitionScreenWipeMaterial;
            }
            set { _transitionScreenWipeMaterial = value; }
        }
        Material _transitionScreenWipeMaterial;

        #endregion Screen Wipe Properties

        #region Auto instantiate singleton

        /// <summary>
        /// Singleton that auto creates a gameobject.
        /// </summary>
        public static TransitionController Instance {
            get
            {
                if (_instance == null)
                {
                    var singleton = new UnityEngine.GameObject("(Beautiful Transitions - Controller)");
                    _instance = singleton.AddComponent<TransitionController>();

                    DontDestroyOnLoad(singleton);
                }
                return _instance;
            }
            private set { _instance = value; }
        }
        static TransitionController _instance;

        public static bool IsActive { get { return Instance != null; } }

        #endregion Auto instantiate singleton

        /// <summary>
        /// Setup components that are used for performing the screen fade. Done here as we only want one instance
        /// </summary>
        public void SetupScreenFadeComponents()
        {
            TransitionScreenFadeGameObject = new UnityEngine.GameObject("(Beautiful Transitions - ScreenFade");
            //TransitionScreenFadeGameObject.transform.SetParent(transform);
            TransitionScreenFadeGameObject.SetActive(false);

            var myCanvas = TransitionScreenFadeGameObject.AddComponent<Canvas>();
            myCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            myCanvas.sortingOrder = 999;

            TransitionScreenFadeRawImage = TransitionScreenFadeGameObject.AddComponent<RawImage>();
        }

        /// <summary>
        /// Setup components that are used for performing the screen wipe. Done here as we only want one instance
        /// </summary>
        public void SetupScreenWipeComponents()
        {
            TransitionScreenWipeGameObject = new UnityEngine.GameObject("(Beautiful Transitions - ScreenWipe");
            //TransitionScreenWipeGameObject.transform.SetParent(transform);
            TransitionScreenWipeGameObject.SetActive(false);

            var myCanvas = TransitionScreenWipeGameObject.AddComponent<Canvas>();
            myCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            myCanvas.sortingOrder = 999;

            TransitionScreenWipeRawImage = TransitionScreenWipeGameObject.AddComponent<RawImage>();

            var shader = Shader.Find("FlipWebApps/BeautifulTransitions/WipeScreen");
            if (shader != null && shader.isSupported)
            {
                TransitionScreenWipeRawImage.material = TransitionScreenWipeMaterial = new Material(shader);
            }
            else
                Debug.Log("WipScreen: Shader is not found or supported on this platform.");
        }
    }
}

