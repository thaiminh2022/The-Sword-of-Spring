using UnityEngine;

using TheSwordOfSpring.AnimationSystem;

namespace TheSwordOfSpring.EnemySystem
{
    public class EnemyAnimation : AnimatorHolder<NormalEnemyAnim>
    {
        public void SetHitAnimation()
        {
            animatorSystem.SetAnimation(NormalEnemyAnim.Hit);
        }
        public void SetRunAnimation()
        {
            animatorSystem.SetAnimation(NormalEnemyAnim.Run);
        }
        public void SetDieAnimation()
        {
            animatorSystem.SetAnimation(NormalEnemyAnim.Dead);
        }
    }

    public enum NormalEnemyAnim
    {
        Dead,
        Hit,
        Run
    }
}