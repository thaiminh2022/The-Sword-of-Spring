using UnityEngine;
using TheSwordOfSpring.Modules;
using TheSwordOfSpring.TimeSystem;

namespace TheSwordOfSpring.UISystem
{
    public class OpenInventory : MonoBehaviour
    {
        IBaseInput baseInput;

        private bool inventorySwitch = false;

        private void Start()
        {
            baseInput = GetComponent<IBaseInput>();
        }

        private void Update()
        {
            if (baseInput.OpenInventory())
            {
                if (inventorySwitch == false)
                {
                    UIManager.Instance.SetInventoryActive(true);
                    ChangeToUIMode();
                    GameTimeManager.Pause();
                    inventorySwitch = true;
                }
            }
            else
            {
                inventorySwitch = false;
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