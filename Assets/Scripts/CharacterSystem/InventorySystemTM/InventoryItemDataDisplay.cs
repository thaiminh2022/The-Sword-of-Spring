using UnityEngine;
using TMPro;
using System;

namespace TheSwordOfSpring.CharacterSystem.InventorySystemTM
{
    public class InventoryItemDataDisplay : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI nameText;
        [SerializeField] TextMeshProUGUI descText;

        private void Start()
        {
            InventoryItemUI.OnItemClicked += InventoryItem_OnItemClicked;
            InventoryUI.OnCloseInventory += InventoryUI_OnCloseInventory;
        }

        private void InventoryUI_OnCloseInventory(object sender, EventArgs args)
        {
            nameText.SetText("");
            descText.SetText("");
        }
        private void InventoryItem_OnItemClicked(object sender, ItemScriptableObject item)
        {
            string itemName = item.name;
            string itemDesc = CreateItemDescString(item);

            nameText.SetText(itemName);
            descText.SetText(itemDesc);
        }

        private string CreateItemDescString(ItemScriptableObject itemBase)
        {
            string itemDesc = "";

            foreach (var buffData in itemBase.GetBuffDatas())
            {
                itemDesc += $"{buffData.buffName}: {buffData.buffAmount}\n";
            }

            itemDesc += itemBase.desc;

            return itemDesc;
        }

        private void OnDestroy()
        {
            InventoryItemUI.OnItemClicked -= InventoryItem_OnItemClicked;

        }

    }
}