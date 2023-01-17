using UnityEngine;
using System.Collections.Generic;
using TheSwordOfSpring.AnimationSystem;

namespace TheSwordOfSpring.CharacterSystem
{
    public class PlayerAnimation : AnimatorHolder<PlayerAnimationClip>
    {

    }

    public enum PlayerAnimationClip
    {
        Run,
        Attack,
        Hit,
        Dead,
        Stand,

    }
}