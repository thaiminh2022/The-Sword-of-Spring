using TheSwordOfSpring.Modules;
using UnityEngine;


namespace TheSwordOfSpring.CharacterSystem
{
    public class CharacterBaseInput : MonoBehaviour, IBaseInput
    {
        [SerializeField] CharacterBase characterBase;

        private PlayerInputActions inputActions;

        private void Start()
        {
            inputActions = characterBase.GetInputActions();
        }

        public bool MouseRightClick()
        {
            return inputActions.Player.RightMouse.IsPressed();
        }

        public Vector2 GetKeyBoardInput()
        {
            return inputActions.Player.KeyboardInput.ReadValue<Vector2>();
        }

        public bool InteractPressed()
        {
            return inputActions.Player.Interact.IsPressed();
        }

        public bool AttackPressed()
        {
            return inputActions.Player.Attack.IsPressed();
        }
    }
}

