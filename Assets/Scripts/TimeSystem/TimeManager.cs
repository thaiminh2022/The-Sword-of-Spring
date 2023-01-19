using System;
using UnityEngine;

namespace TheSwordOfSpring.TimeSystem
{
    public class TimeManager : MonoBehaviour
    {
        public static int Day;
        public static int Hour = 6;
        public static bool IsNight = false;


        [SerializeField] private float startSecondsPerHour;
        private float secondsPerHour;


        public const int StartDayHour = 6;
        public const int StartNightHour = 18;

        private bool dayEventInvoked = false;


        public static event EventHandler OnNextHour;
        public static event EventHandler OnNextDay;
        public static event EventHandler OnDay;
        public static event EventHandler OnNight;


        private void Start()
        {
            secondsPerHour = startSecondsPerHour;
        }

        private void Update()
        {
            if (secondsPerHour <= 0)
            {
                CheckHour();
                CheckDay();
                secondsPerHour = startSecondsPerHour;
            }
            else
            {
                secondsPerHour -= Time.deltaTime;
            }
        }
        private void CheckDay()
        {
            if (Hour % 24 == 0)
            {
                // A new day has pass
                Day++;
                OnNextDay?.Invoke(this, EventArgs.Empty);
                Hour = 0;
            }
        }
        private void CheckHour()
        {
            Hour++;
            OnNextHour?.Invoke(this, EventArgs.Empty);

            if (InRange(StartDayHour, StartNightHour, Hour))
            {

                if (!dayEventInvoked)
                {
                    OnDay?.Invoke(this, EventArgs.Empty);
                    dayEventInvoked = true;
                }

                IsNight = false;
                return;
            }

            // It's night now
            IsNight = true;

            if (dayEventInvoked == true)
            {
                OnNight?.Invoke(this, EventArgs.Empty);
                dayEventInvoked = false;
            }
        }

        public static int GetDifferencesHourFromStartNight()
        {

            if (Hour >= 0 && Hour < StartNightHour)
            {
                return Mathf.Abs(24 - StartNightHour + Hour);
            }
            return Mathf.Abs(Hour - StartNightHour);
        }

        public static int GetDifferencesHourFromStartDay()
        {

            if (Hour >= 0 && Hour < StartDayHour)
            {
                return Mathf.Abs(24 - StartDayHour + Hour);
            }
            return Mathf.Abs(Hour - StartDayHour);
        }
        public static int GetNightLength()
        {
            return 24 - GetDayLength();
        }
        public static int GetDayLength()
        {
            return StartNightHour - StartDayHour;
        }

        public static float GetHourNormalize()
        {
            return Hour / 24f;
        }
        public static float GetNightHourNormalized()
        {
            return GetDifferencesHourFromStartNight() / GetNightLength();
        }
        public static float GetDayHourNormalized()
        {
            return GetDifferencesHourFromStartDay() / GetDayLength();
        }

        private bool InRange(int start, int end, int value)
        {
            return (start <= value) && (value <= end);
        }
    }
}
