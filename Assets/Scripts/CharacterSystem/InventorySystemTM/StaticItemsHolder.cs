using UnityEngine;
using TheSwordOfSpring.Misc;

namespace TheSwordOfSpring.CharacterSystem.InventorySystemTM
{
    [CreateAssetMenu(menuName = "TheSwordOfSpring/Datas/ItemsHolder", fileName = "StaticItemsHolder")]
    public class StaticItemsHolder : SingletonScriptableObject<StaticItemsHolder>
    {
        public ItemScriptableObject ChungCakeCoin;
        public ItemScriptableObject ChungCakeNormal;
        public ItemScriptableObject EnvelopeCoin;
        public ItemScriptableObject EnvelopeNormal;
        public ItemScriptableObject EnvelopeFish;
        public ItemScriptableObject ParentFolded;
        public ItemScriptableObject ParentNormal;
        public ItemScriptableObject HeartOfFarmer;
        public ItemScriptableObject HeartOfSpring;



    }
}

