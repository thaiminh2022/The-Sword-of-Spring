using UnityEngine;
using System;
using UnityEngine.UI;
using TheSwordOfSpring.UISystem;
using TheSwordOfSpring.TimeSystem;

namespace TheSwordOfSpring.CharacterSystem.InventorySystemTM
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField]
        private Button closeButton;

        public static event EventHandler OnCloseInventory;

        private void Start()
        {
            closeButton.onClick.AddListener(() =>
            {
                OnCloseInventory?.Invoke(this, EventArgs.Empty);
                UIManager.UsePlayerMode();

                GameTimeManager.Resume();
                gameObject.SetActive(false);
            });
        }
    }
}