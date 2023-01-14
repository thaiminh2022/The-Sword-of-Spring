using UnityEngine;


namespace TheSwordOfSpring.CharacterSystem
{
    [CreateAssetMenu(fileName = "Character", menuName = "TheSwordOfSpring/Character"), System.Serializable]
    public class CharacterScriptableObject : ScriptableObject
    {
        [Header("Desc")]
        public new string name;
        [TextArea(3, 5)]
        public string description;
        public Sprite sprite;

        [Header("General"), Space()]
        public float health;
        public float viewRange;

        [Header("Combat"), Space()]
        public float damage;
        public float atkRange;
        public float atkSpeed;

        public float moveSpeed;


    }

}


