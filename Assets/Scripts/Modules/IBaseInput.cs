using UnityEngine;

namespace TheSwordOfSpring.Modules
{
    public interface IBaseInput
    {
        bool MouseRightClick();
        bool InteractPressed();
        bool AttackPressed();
        bool OpenInventory();
        bool PauseUI();

        Vector2 GetKeyBoardInput();
    }
}

