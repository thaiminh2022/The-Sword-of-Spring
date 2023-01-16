using UnityEngine;

namespace TheSwordOfSpring.Modules
{
    public interface IBaseInput
    {
        bool MouseRightClick();
        bool InteractPressed();
        bool AttackPressed();
        bool OpenInventory();

        Vector2 GetKeyBoardInput();
    }
}

