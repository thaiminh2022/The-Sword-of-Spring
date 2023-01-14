using UnityEngine;
using TMPro;
using System;

namespace TheSwordOfSpring.TimeSystem
{
    public class TimeTextUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI clockText;


        private void Start()
        {
            TimeManager.OnNextHour += TimeManager_OnNextHour;
            TimeManager.OnNextDay += TimeManager_OnNextDay;

            UpdateNextDayText();
            UpdateNextHourText();

        }

        private void TimeManager_OnNextHour(object sender, EventArgs args)
        {
            if (TimeManager.Hour != 0 || TimeManager.Hour != 24)
            {
                UpdateNextHourText();
            }
        }
        private void TimeManager_OnNextDay(object sender, EventArgs args)
        {
            UpdateNextDayText();
        }

        private void UpdateNextDayText()
        {
            clockText.SetText($"Day: {TimeManager.Day:00}");
        }
        private void UpdateNextHourText()
        {
            clockText.SetText($"{TimeManager.Hour:00}:00");
        }

        private void OnDestroy()
        {
            TimeManager.OnNextHour += TimeManager_OnNextHour;
            TimeManager.OnNextDay += TimeManager_OnNextDay;
        }
    }
}