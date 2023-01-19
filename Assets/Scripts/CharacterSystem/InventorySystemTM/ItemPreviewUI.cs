using UnityEngine;
using TheSwordOfSpring.TimeSystem;
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
                GameTimeManager.Resume();
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
            GameTimeManager.Pause();
            gameObject.SetActive(true);
            nameText.SetText(itemBase.name);
            descText.SetText(CreateItemDescString(itemBase));
            itemSpritePreview.sprite = itemBase.sprite;
            UIManager.UseUIMode();

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
            PickupableInventoryItem.OnItemPickUp -= PickupableInventoryItem_OnItemPickUp;

        }
    }
}