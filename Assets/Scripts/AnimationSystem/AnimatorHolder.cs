using UnityEngine;
using System.Collections.Generic;
using System;

namespace TheSwordOfSpring.AnimationSystem
{
    public class AnimatorHolder<TAnimations> : MonoBehaviour where TAnimations : Enum
    {
        [SerializeField] protected Animator animator;
        protected AnimatorSystem<TAnimations> animatorSystem;
        private void Awake()
        {
            animatorSystem = new AnimatorSystem<TAnimations>(animator);
        }

        public void StopAllAnimation()
        {
            animatorSystem.StopAllAnimation();
        }


        private void OnValidate()
        {
            if (animator == null)
            {
                animator = GetComponent<Animator>();
            }
        }
    }
}