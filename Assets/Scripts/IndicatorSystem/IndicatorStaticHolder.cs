using UnityEngine;
using TheSwordOfSpring.Misc;


namespace TheSwordOfSpring.IndicatorSystem
{
    [CreateAssetMenu(menuName = "TheSwordOfSpring/Datas/IndicatorsHolder", fileName = "IndicatorsHolder")]
    public class IndicatorStaticHolder : SingletonScriptableObject<IndicatorStaticHolder>
    {
        public GameObject circle;
        public GameObject square;

    }
}
