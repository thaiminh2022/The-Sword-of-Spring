using UnityEngine;

namespace TheSwordOfSpring.Misc
{
    [CreateAssetMenu(menuName = "TheSwordOfSpring/Datas/PSHolder", fileName = "PSHolder")]
    public class PSStaticHolder : SingletonScriptableObject<PSStaticHolder>
    {
        public GameObject Fire_PS;
        public GameObject Stun_PS;

        public GameObject Explosion_PS;
        public GameObject Dash_PS;


    }
}