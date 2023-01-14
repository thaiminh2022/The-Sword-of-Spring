using UnityEngine;

namespace TheSwordOfSpring.WeaponSystem
{
    [CreateAssetMenu(menuName = "TheSwordOfSpring/Weapon", fileName = "WeaponScriptableObject")]
    public class WeaponScriptableObject : ScriptableObject
    {
        [Header("Desc")]
        public new string name;
        public string decs;
        public Sprite sprite;

        [Header("Combat")]

        public float damageMultiplier;
    }
}