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

namespace FlipWebApps.BeautifulTransitions.Scripts.Transitions.TransitionSteps
{
    /// <summary>
    /// Transition step for wiping the screen.
    /// </summary>
    public class ScreenWipe : TransitionStepScreen {
        public Texture2D Texture;
        public Color Color;
        public Texture2D MaskTexture;
        public bool InvertMask;

        #region Constructors

        public ScreenWipe(UnityEngine.GameObject target,
            Texture2D maskTexture,
            bool invertMask = false,
            Color? color = null,
            Texture2D texture = null,
            float delay = 0,
            float duration = 0.5f,
            TransitionHelper.TweenType tweenType = TransitionHelper.TweenType.linear,
            AnimationCurve animationCurve = null,
            Action onStart = null,
            Action<float> onUpdate = null,
            Action onComplete = null) :
                base(target, delay: delay, duration: duration, tweenType: tweenType,
                animationCurve: animationCurve, onStart: onStart,onUpdate: onUpdate, onComplete: onComplete)
        {
            MaskTexture = maskTexture;
            InvertMask = invertMask;
            Color = color.HasValue ? color.Value : Color.white;
            Texture = texture;
        }

        #endregion Constructors

        #region TransitionStep Overrides

        /// <summary>
        /// Call to start the transition
        /// </summary>
        public override void Start()
        {
            SetConfiguration(Texture, Color, MaskTexture, InvertMask);
            base.Start();
        }

        /// <summary>
        /// Set the current transparency level
        /// </summary>
        /// <param name="position"></param>
        public override void SetCurrent(float progress)
        {
//            // return if editor and no attached RawImage
//#if UNITY_EDITOR
//            if (!Application.isPlaying && RawImage == null) return;
//#endif

            TransitionController.Instance.TransitionScreenWipeMaterial.SetFloat("_Amount", Value);
        }

        #endregion TransitionStep Overrides

        void SetConfiguration(Texture2D texture, Color color, Texture2D maskTexture, bool invertMask)
        {
            TransitionController.Instance.TransitionScreenWipeRawImage.texture = texture;
            TransitionController.Instance.TransitionScreenWipeMaterial.SetColor("_Color", color);
            TransitionController.Instance.TransitionScreenWipeMaterial.SetTexture("_MaskTex", maskTexture);
            if (invertMask)
                TransitionController.Instance.TransitionScreenWipeMaterial.EnableKeyword("INVERT_MASK");
            else
                TransitionController.Instance.TransitionScreenWipeMaterial.DisableKeyword("INVERT_MASK");
        }


        protected override void SetTransitionScreenEnabledState(bool state)
        {
            if (RawImage != null)
                base.SetTransitionScreenEnabledState(state);
            else
                TransitionController.Instance.TransitionScreenWipeGameObject.SetActive(state);
        }
    }

    #region TransitionStep extensions

    public static class ScreenWipeExtensions
    {
        /// <summary>
        /// Fade extension method for TransitionStep
        /// </summary>
        /// <typeparam name="T">interface type</typeparam>
        /// <param name="TransitionStep"></param>
        /// <returns></returns>
        public static ScreenWipe ScreenWipe(this TransitionStep transitionStep,
            Texture2D maskTexture,
            bool invertMask = false,
            Color? color = null,
            Texture2D texture = null,
            float delay = 0,
            float duration = 0.5f,
            TransitionHelper.TweenType tweenType = TransitionHelper.TweenType.linear,
            AnimationCurve animationCurve = null,
            Action onStart = null,
            Action<float> onUpdate = null,
            Action onComplete = null)
        {
            var newTransitionStep = new ScreenWipe(transitionStep.Target,
                maskTexture,
                invertMask,
                color,
                texture,
                delay,
                duration,
                tweenType,
                animationCurve,
                onStart,
                onUpdate,
                onComplete);
            transitionStep.AddOnCompleteTransitionStep(newTransitionStep);
            newTransitionStep.Parent = transitionStep;
            return newTransitionStep;
        }
    }
    #endregion TransitionStep extensions
}
