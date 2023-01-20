using UnityEngine;
using System.Collections.Generic;
using TheSwordOfSpring.Modules;
using TheSwordOfSpring.TimeSystem;

namespace TheSwordOfSpring.UISystem
{
    public class PauseGame : MonoBehaviour
    {
        IBaseInput baseInput;

        private bool pauseGameSwitch = false;

        private void Start()
        {
            baseInput = GetComponent<IBaseInput>();
        }

        private void Update()
        {
            if (baseInput.PauseUI())
            {
                if (pauseGameSwitch == false)
                {
                    UIManager.Instance.SetPauseUIActive(true);
                    ChangeToUIMode();
                    GameTimeManager.Pause();
                    pauseGameSwitch = true;
                }
            }
            else
            {
                pauseGameSwitch = false;
            }

        }

        private void ChangeToUIMode()
        {
            if (UIManager.GetUIMode() != InputMode.UI)
            {
                UIManager.UseUIMode();
            }
        }
        private void ChangeToPlayerMode()
        {
            if (UIManager.GetUIMode() != InputMode.Player)
            {
                UIManager.UsePlayerMode();
            }
        }
    }
}