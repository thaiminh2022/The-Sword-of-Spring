using UnityEngine;
namespace TheSwordOfSpring.TimeSystem
{

    public class GameTimeManager
    {
        private static float defaultSimulationTime = 1;
        private static float lastSimulationTime = 1;

        public static void Pause()
        {
            lastSimulationTime = Time.timeScale;
            Time.timeScale = 0;
        }

        public static void Resume()
        {
            Time.timeScale = lastSimulationTime;
        }
    }
}