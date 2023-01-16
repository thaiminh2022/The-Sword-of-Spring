using UnityEngine;
using TheSwordOfSpring.Modules;

namespace TheSwordOfSpring.UISystem
{
    public class OpenInventory : MonoBehaviour
    {
        IBaseInput baseInput;

        private bool inventorySwitch = false;
        private bool isAlreadyOpen = false;

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
                    isAlreadyOpen = !isAlreadyOpen;

                    if (isAlreadyOpen == true)
                    {
                        ChangeToUIMode();
                    }
                    else
                    {
                        ChangeToPlayerMode();
                    }

                    UIManager.Instance.SetInventoryActive(isAlreadyOpen);
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