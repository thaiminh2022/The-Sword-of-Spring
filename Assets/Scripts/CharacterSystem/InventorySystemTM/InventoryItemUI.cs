using UnityEngine;
using System.Collections.Generic;
using Redcode.Extensions;
using TMPro;
using UnityEngine.UI;
using System;

namespace TheSwordOfSpring.CharacterSystem.InventorySystemTM
{
    public class InventoryItemUI : MonoBehaviour
    {
        [Header("Update Item")]
        [SerializeField] Transform itemsHolder;
        [SerializeField] GameObject itemsTemplate;
        [SerializeField] CharacterBase characterBase;

        private InventorySystem inventorySystem;

        public static event EventHandler<ItemScriptableObject> OnItemClicked;


        private void Start()
        {
            inventorySystem = characterBase.GetInventory();
            PickupableInventoryItem.OnItemPickUp += PickupableInventoryItem_OnItemPickup;

            UpdateItems();
        }

        private void PickupableInventoryItem_OnItemPickup(object sender, ItemScriptableObject item)
        {
            UpdateItems();
        }

        private void UpdateItems()
        {
            DestroyAllChild();

            var items = inventorySystem.GetItems();

            foreach (var item in items)
            {
                var go = Instantiate(itemsTemplate, itemsHolder);
                var template = go.GetComponent<InventoryItemUITemplate>();
                var itemBase = item.GetItemBase();

                template.inventoryItemBtn.onClick.AddListener(() =>
                {
                    OnItemClicked?.Invoke(this, itemBase);
                });

                template.inventoryItemImage.sprite = item.GetItemBase().sprite;
            }


        }

        private void DestroyAllChild()
        {
            itemsHolder.DestroyChilds();

        }


    }
}