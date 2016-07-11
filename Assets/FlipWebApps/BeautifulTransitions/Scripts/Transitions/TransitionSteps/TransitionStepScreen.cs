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

using System;
using UnityEngine;
using UnityEngine.UI;

namespace FlipWebApps.BeautifulTransitions.Scripts.Transitions.TransitionSteps
{
    /// <summary>
    /// A screen transition step base class
    /// </summary>
    public class TransitionStepScreen : TransitionStepFloat {

        protected RawImage RawImage;
        protected Material Material;

        #region Constructors

        public TransitionStepScreen(UnityEngine.GameObject target,
            float delay = 0,
            float duration = 0.5f,
            TransitionHelper.TweenType tweenType = TransitionHelper.TweenType.linear,
            AnimationCurve animationCurve = null,
            Action onStart = null,
            Action<float> onUpdate = null,
            TransitionStep onCompleteItem = null,
            Action onComplete = null,
            Action<object> onCompleteWithData = null,
            object onCompleteData = null) :
                base(target, delay: delay, duration: duration, tweenType: tweenType,
                animationCurve: animationCurve, onStart: onStart,onUpdate: onUpdate, onComplete: onComplete)
        {
            SetupComponentReferences();
        }

        #endregion Constructors


        #region TransitionStep Overrides

        /// <summary>
        /// Call to start the transition
        /// </summary>
        public override void Start()
        {
            SetTransitionScreenEnabledState(true);
            base.Start();
        }

        public override void Completed()
        {
            // Only disable the image if we have transitioned in.
            if (Mathf.Approximately(EndValue, 0))
                SetTransitionScreenEnabledState(false);
            base.Completed();
        }

        #endregion TransitionStep Overrides

        /// <summary>
        /// Get component references
        /// </summary>
        private void SetupComponentReferences()
        {
            // see if we have our own RamImage, if not then we use the global one.
            RawImage = Target.GetComponent<RawImage>();
            if (RawImage != null)
            {
                RawImage.enabled = false;
                Material = RawImage.material;
            }
        }


        protected virtual void SetTransitionScreenEnabledState(bool state)
        {
            if (RawImage != null)
                RawImage.enabled = state;
        }
    }
}
