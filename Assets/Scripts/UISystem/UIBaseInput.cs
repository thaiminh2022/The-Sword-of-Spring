using UnityEngine;
using System.Collections.Generic;
using TheSwordOfSpring.Modules;
using TheSwordOfSpring.CharacterSystem;

namespace TheSwordOfSpring.UISystem
{
    public class UIBaseInput : MonoBehaviour, IUIInput
    {
        private PlayerInputActions playerInputAction;
        private void Start()
        {
            playerInputAction = CharacterBase.GetInputActions();
        }
        public bool EscapeUIMode()
        {
            return playerInputAction.UIMap.ExitUIMode.IsPressed();
        }
    }
}