using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using TheSwordOfSpring.UISystem;

namespace TheSwordOfSpring.CharacterSystem.InventorySystemTM
{
    public class ItemPreviewUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI descText;

        [SerializeField] private Image itemSpritePreview;
        [SerializeField] private Button closeBtn;

        private void Awake()
        {
            PickupableInventoryItem.OnItemPickUp += PickupableInventoryItem_OnItemPickUp;
            closeBtn.onClick.AddListener(() =>
            {
                nameText.SetText("");
                descText.SetText("");
                itemSpritePreview.sprite = null;

                UIManager.UsePlayerMode();
                gameObject.SetActive(false);
            });
        }
        private void Start()
        {
            gameObject.SetActive(false);
        }

        private void PickupableInventoryItem_OnItemPickUp(object sender, ItemScriptableObject itemBase)
        {
            gameObject.SetActive(true);
            nameText.SetText(itemBase.name);
            descText.SetText(itemBase.desc);
            itemSpritePreview.sprite = itemBase.sprite;
            UIManager.UseUIMode();

        }
        private void OnDestroy()
        {
            PickupableInventoryItem.OnItemPickUp -= PickupableInventoryItem_OnItemPickUp;

        }
    }
}