using System.Linq;
using System;
using UnityEngine;
using UnityEngine.UI;
using TheSwordOfSpring.UISystem;
using TheSwordOfSpring.TimeSystem;
using Redcode.Extensions;
using TheSwordOfSpring.CharacterSystem;
using TheSwordOfSpring.CharacterSystem.InventorySystemTM;

namespace TheSwordOfSpring.SceneSystem
{

    public class RequireItemsToBossUI : MonoBehaviour
    {
        [SerializeField]
        private RequiresItemToBoss requiresItemToBoss;

        [SerializeField]
        private GameObject templateObj;

        [SerializeField]
        private GameObject UIobject;

        [SerializeField]
        private Button submitButton;
        [SerializeField]
        private Button closeButton;



        private void Start()
        {

            closeButton.onClick.AddListener(() =>
            {
                gameObject.SetActive(false);
                UIManager.UsePlayerMode();
                GameTimeManager.Resume();

            });

            submitButton.onClick.AddListener(() =>
            {
                var requireItems = requiresItemToBoss.GetRequireItems();
                var hasItems = requiresItemToBoss.GetHasItems();


                bool enoughItems = requireItems.All(hasItems.Contains);

                if (enoughItems)
                {
                    // Change scene to boss
                    Debug.Log("Cool, let's go boss battle");
                    UIManager.UsePlayerMode();
                    gameObject.SetActive(false);
                    GameTimeManager.Resume();

                    GameObject.FindGameObjectWithTag("Player")?
                    .GetComponent<CharacterBase>()
                    .GetInventory()
                    .AddItem(new InventoryItem(StaticItemsHolder.Instance.HeartOfSpring));

                    ScenesManager.ToBossIScene();
                }
                GameTimeManager.Resume();


            });
            requiresItemToBoss.OnInteract += RequiresItemToBoss_OnInteract;

            gameObject.SetActive(false);
        }

        private void RequiresItemToBoss_OnInteract(object sender, EventArgs args)
        {
            gameObject.SetActive(true);
            UIManager.UseUIMode();
            GameTimeManager.Pause();

            var requireItems = requiresItemToBoss.GetRequireItems();
            var hasItems = requiresItemToBoss.GetHasItems();

            UIobject.transform.DestroyChilds();
            foreach (var item in requireItems)
            {
                GameObject go = Instantiate(templateObj, Vector3.zero, Quaternion.identity, UIobject.transform);
                // Get template
                var template = go.GetComponent<RequireItemTemplate>();
                int amount = hasItems.FindAll(itemData => itemData.GetItemBase() == item.GetItemBase()).Count;

                template.amountText.SetText($"{amount}/1");
                template.spriteImage.sprite = item.GetItemBase().sprite;

                // Set up template
            }


        }


    }
}