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

using FlipWebApps.BeautifulTransitions.Scripts.Transitions.TransitionSteps;
using UnityEngine;
using UnityEngine.Events;

namespace FlipWebApps.BeautifulTransitions.Scripts.Transitions
{
    /// <summary>
    /// Abstract base class for all transition components.
    /// </summary>
    public abstract class TransitionBase : MonoBehaviour {
        [Tooltip("Whether to set up ready for transitioning in.")]
        public bool InitForTransitionIn = true;
        [Tooltip("Whether to automatically run the transition in effect on start.")]
        public bool AutoRun;

        public TransitionSettings TransitionInConfig;
        public TransitionSettings TransitionOutConfig;

        public enum TransitionModeType {None, In, Out}
        public TransitionModeType TransitionMode { get; set; }

        public TransitionStep CurrentTransitionStep { get; set; }

        /// <summary>
        /// initialisation and default auto run
        /// </summary>
        public virtual void Start()
        {
            #if UNITY_EDITOR
            if (!Application.isPlaying) return;
            #endif

            if (InitForTransitionIn || AutoRun)
            {
                InitTransitionIn();
            }

            if (AutoRun)
            {
                TransitionIn();
            }
        }

        /// <summary>
        /// Initialise for a transition in.
        /// </summary>
        public virtual void InitTransitionIn()
        {
            TransitionMode = TransitionModeType.In;
            CurrentTransitionStep = CreateTransitionStepIn();
            CurrentTransitionStep.SetProgressToStart();
        }


        /// <summary>
        /// Transition in.
        /// </summary>
        public virtual void TransitionIn()
        {
            InitTransitionIn();
            CurrentTransitionStep.Start();
        }


        /// <summary>
        /// Initialise for a transition out.
        /// </summary>
        public virtual void InitTransitionOut()
        {
            TransitionMode = TransitionModeType.Out;
            CurrentTransitionStep = CreateTransitionStepOut();
            CurrentTransitionStep.SetProgressToStart();
        }


        /// <summary>
        /// Transition out.
        /// </summary>
        public virtual void TransitionOut()
        {
            InitTransitionOut();
            CurrentTransitionStep.Start();
        }

        /// <summary>
        /// Override this if you need to do any specific action when the value is updated
        /// </summary>
        /// <param name="amount"></param>
        public virtual void ValueUpdated(float amount)
        {
        }

        #region Transition Callbacks

        /// <summary>
        /// Called when an in transition is started
        /// </summary>
        public virtual void TransitionInStart()
        {
            if (TransitionInConfig.OnTransitionStart != null)
                TransitionInConfig.OnTransitionStart.Invoke();
        }


        /// <summary>
        /// Called when an out transition is started
        /// </summary>
        public virtual void TransitionOutStart()
        {
            if (TransitionOutConfig.OnTransitionStart != null)
                TransitionOutConfig.OnTransitionStart.Invoke();
        }

        /// <summary>
        /// Called when an in transition has been completed (or interupted)
        /// </summary>
        public virtual void TransitionInComplete()
        {
            TransitionMode = TransitionModeType.Out;
            if (TransitionInConfig.OnTransitionComplete != null)
                TransitionInConfig.OnTransitionComplete.Invoke();
        }


        /// <summary>
        /// Called when an out transition has been completed (or interupted)
        /// </summary>
        public virtual void TransitionOutComplete()
        {
            if (TransitionOutConfig.OnTransitionComplete != null)
                TransitionOutConfig.OnTransitionComplete.Invoke();
        }

        #endregion transition callbacks

        #region Create transitionStep

        /// <summary>
        /// Create a transitionStep. Implement this to create the correct subclass of transitionStep
        /// </summary>
        /// <returns></returns>
        public abstract TransitionStep CreateTransitionStep();

        /// <summary>
        /// Create a transitionStep for transitioning in and populate with configured values
        /// </summary>
        /// <returns></returns>
        public virtual TransitionStep CreateTransitionStepIn()
        {
            var transitionStep = CreateTransitionStep();
            SetupTransitionStepIn(transitionStep);
            return transitionStep;
        }

        /// <summary>
        /// Add common values to the transitionStep for the in transition
        /// </summary>
        /// <param name="transitionStep"></param>
        public virtual void SetupTransitionStepIn(TransitionStep transitionStep)
        {
            transitionStep.Delay = TransitionInConfig.Delay;
            transitionStep.Duration = TransitionInConfig.Duration;
            transitionStep.TweenType = TransitionInConfig.TransitionType;
            transitionStep.AnimationCurve = TransitionInConfig.AnimationCurve;
            transitionStep.OnStart += TransitionInStart;
            transitionStep.OnComplete += TransitionInComplete;
            transitionStep.OnUpdate += ValueUpdated;
        }

        /// <summary>
        /// Create a transitionStep for transitioning out and populate with configured values
        /// </summary>
        /// <returns></returns>
        public virtual TransitionStep CreateTransitionStepOut()
        {
            var transitionStep = CreateTransitionStep();
            SetupTransitionStepOut(transitionStep);
            return transitionStep;
        }

        /// <summary>
        /// Add common values to the transitionStep for the out transition
        /// </summary>
        /// <param name="transitionStep"></param>
        public virtual void SetupTransitionStepOut(TransitionStep transitionStep)
        {
            transitionStep.Delay = TransitionOutConfig.Delay;
            transitionStep.Duration = TransitionOutConfig.Duration;
            transitionStep.TweenType = TransitionOutConfig.TransitionType;
            transitionStep.AnimationCurve = TransitionOutConfig.AnimationCurve;
            transitionStep.OnStart += TransitionOutStart;
            transitionStep.OnComplete += TransitionOutComplete;
            transitionStep.OnUpdate += ValueUpdated;
        }

        #endregion Create transitionStep

        /// <summary>
        /// Transition setting calss exposed through the editor
        /// </summary>
        [System.Serializable]
        public class TransitionSettings
        {
            [Tooltip("Whether to automatically check and run transitions on child GameObjects.")]
            public bool TransitionChildren = false;
            [Tooltip("Whether this must be transitioned specifically. If not set it will run automatically when a parent transition is run that has the TransitionChildren property set.")]
            public bool MustTriggerDirect = false;
            [Tooltip("Time in seconds before this transition should be started.")]
            public float Delay;
            [Tooltip("How long this transition will / should run for.")]
            public float Duration = 0.3f;
            [Tooltip("How the transition should be run.")]
            public TransitionHelper.TweenType TransitionType = TransitionHelper.TweenType.linear;
            [Tooltip("A custom curve to show how the transition should be run.")]
            public AnimationCurve AnimationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
            [Tooltip("Methods that should be called when the transition is started.")]
            public UnityEvent OnTransitionStart;
            [Tooltip("Methods that should be called when the transition has completed.")]
            public UnityEvent OnTransitionComplete;
        }
    }
}
