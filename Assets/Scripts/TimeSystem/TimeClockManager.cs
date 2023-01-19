using System;
using UnityEngine;

namespace TheSwordOfSpring.TimeSystem
{
    public class TimeClockManager : MonoBehaviour
    {
        public static float CurrentClockDegree { get; private set; }

        private void Start()
        {
            TimeManager.OnNextHour += TimeManager_OnNextHour;
            CalculateDegree();
        }

        private void TimeManager_OnNextHour(object sender, EventArgs args)
        {
            CalculateDegree();
        }
        private void CalculateDegree()
        {
            float normalizedHour = TimeManager.GetHourNormalize();

            // Convert to percentage + to degree
            float degree = normalizedHour * 100 * 3.6f;

            // print($"Hour: {TimeManager.Hour}, degree: {degree}");
            CurrentClockDegree = degree;
        }

        private void OnDestroy()
        {
            TimeManager.OnNextHour -= TimeManager_OnNextHour;
        }
    }
}