using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Redcode.Extensions;

namespace TheSwordOfSpring.CharacterSystem.InventorySystemTM
{
    public class InventoryItemUITemplate : MonoBehaviour
    {
        public Button inventoryItemBtn;
        public Image inventoryItemImage;

        private void OnValidate()
        {
            if (inventoryItemBtn == null)
            {
                inventoryItemBtn = GetComponent<Button>();
            }
            if (inventoryItemImage == null)
            {
                transform.DestroyChilds();
                inventoryItemImage = transform.GetChild(0).GetComponent<Image>();
            }
        }

    }
}