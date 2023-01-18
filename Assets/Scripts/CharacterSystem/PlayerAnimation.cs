using UnityEngine;
using System.Collections.Generic;
using TheSwordOfSpring.AnimationSystem;

namespace TheSwordOfSpring.CharacterSystem
{
    public class PlayerAnimation : AnimatorHolder<PlayerAnimationClip>
    {
        public void SetAttackAnimation()
        {
            animatorSystem.SetAnimation(PlayerAnimationClip.Attack, 1);
        }
        public void SetHitAnimation()
        {
            animatorSystem.SetAnimation(PlayerAnimationClip.Hit, 1);
        }
        public void SetRunAnimation()
        {
            animatorSystem.SetAnimation(PlayerAnimationClip.Run);
        }
        public void SetDeadAnimation()
        {
            animatorSystem.SetAnimation(PlayerAnimationClip.Dead);
        }
        public void SetIdleAnimation()
        {
            animatorSystem.SetAnimation(PlayerAnimationClip.Idle);
        }

        public bool IsAnimationFinish(PlayerAnimationClip clip, int layer = 0)
        {
            return !animatorSystem.AnimatorIsPlaying(clip, layer);
        }
    }

    public enum PlayerAnimationClip
    {
        Attack,
        Hit,
        Run,
        Dead,
        Idle,
    }
}