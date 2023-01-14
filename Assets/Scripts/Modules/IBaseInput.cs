using UnityEngine;

namespace TheSwordOfSpring.Modules
{
    public interface IBaseInput
    {
        public bool MouseRightClick();
        public bool InteractPressed();
        public bool AttackPressed();

        public Vector2 GetKeyBoardInput();
    }
}

