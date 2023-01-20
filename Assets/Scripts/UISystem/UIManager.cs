using UnityEngine;
using System.Collections.Generic;
using TheSwordOfSpring.Modules;
using TheSwordOfSpring.CharacterSystem;

namespace TheSwordOfSpring.UISystem
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

        private static PlayerInputActions playerInputActions;
        private static InputMode inputMode;

        [SerializeField] public GameObject inventoryUI;
        [SerializeField] public GameObject pauseUI;

        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            playerInputActions = CharacterBase.GetInputActions();
        }

        public static void UseUIMode()
        {
            playerInputActions.Player.Disable();
            playerInputActions.UIMap.Enable();
        }
        public static void UsePlayerMode()
        {
            playerInputActions.Player.Enable();
            playerInputActions.UIMap.Disable();
        }
        public static InputMode GetUIMode()
        {
            if (playerInputActions.Player.enabled)
            {
                return InputMode.Player;
            }
            return InputMode.UI;
        }

        public void SetInventoryActive(bool active)
        {
            inventoryUI.SetActive(active);
        }
        public void SetPauseUIActive(bool active)
        {
            pauseUI.SetActive(active);
        }


    }

    public enum InputMode
    {
        UI,
        Player
    }
}