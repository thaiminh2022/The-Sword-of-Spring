using UnityEngine;
using System.Collections.Generic;
using System;

namespace TheSwordOfSpring.AnimationSystem
{
    public class AnimatorSystem<TAnimations> where TAnimations : Enum
    {
        public Animator animator { get; private set; }

        TAnimations currentAnimation;
        TAnimations previousAnimation;


        public event EventHandler<AnimationChangeEventArgs> OnAnimationChange;

        public AnimatorSystem(Animator animator)
        {
            this.animator = animator;
        }

        public void SetAnimation(TAnimations animName, int layer = 0)
        {
            previousAnimation = currentAnimation;
            currentAnimation = animName;
            OnAnimationChange?.Invoke(this, new AnimationChangeEventArgs(animName, previousAnimation));

            animator.enabled = true;
            animator.Play(animName.ToString(), layer);
        }

        public void SetAnimationUnique(TAnimations animName, int layer = 0)
        {
            if (!AnimatorIsPlaying(animName))
            {
                SetAnimation(animName, layer);
            }

        }

        public void StopAllAnimation()
        {
            animator.enabled = false;
        }


        public bool AnimatorIsPlaying()
        {
            return animator.GetCurrentAnimatorStateInfo(0).length >
                   animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        }

        public bool AnimatorIsPlaying(TAnimations stateName, int layer = 0)
        {
            return AnimatorIsPlaying() && animator.GetCurrentAnimatorStateInfo(layer).IsName(stateName.ToString());
        }

        public class AnimationChangeEventArgs : EventArgs
        {
            public TAnimations nextAnimation;
            public TAnimations previousAnimation;

            public AnimationChangeEventArgs(TAnimations nextAnimation, TAnimations lastAnimation)
            {
                this.nextAnimation = nextAnimation;
                this.previousAnimation = lastAnimation;
            }
        }
    }


}