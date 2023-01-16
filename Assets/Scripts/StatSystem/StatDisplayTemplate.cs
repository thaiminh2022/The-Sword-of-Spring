using UnityEngine;
using TMPro;

namespace TheSwordOfSpring.StatSystem
{
    public class StatDisplayTemplate : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI statName;
        [SerializeField] TextMeshProUGUI statValue;


        private void OnValidate()
        {
            if (statName == null || statValue == null)
            {
                var statTexts = transform.GetComponentsInChildren<TextMeshProUGUI>();
                print(statTexts.Length);

                statName = statTexts[0];
                statValue = statTexts[1];

            }
        }

        public void SetStatName(string name)
        {
            statName.SetText(name);
        }
        public void SetStatValue(float amount)
        {
            statValue.SetText(amount.ToString());

        }
    }
}