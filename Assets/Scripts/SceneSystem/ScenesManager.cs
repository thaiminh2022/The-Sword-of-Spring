using UnityEngine;
using UnityEngine.SceneManagement;

namespace TheSwordOfSpring.SceneSystem
{
    public class ScenesManager
    {
        public static void SwitchScene(SceneNames newScene)
        {
            SceneManager.LoadScene(newScene.ToString());
        }
        public static void RestartCurrentScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        public static void ExitApplication()
        {
            Application.Quit();
        }

        public static void ToRestartScene()
        {
            SwitchScene(SceneNames.Restart);
        }
        public static void ToVictoryScene()
        {
            SwitchScene(SceneNames.Victory);
        }
        public static void ToBossIScene()
        {
            SwitchScene(SceneNames.BossI);
        }
        public static void ToMainGameScene()
        {
            SwitchScene(SceneNames.MainGame);
        }

        public static string GetSceneName()
        {
            return SceneManager.GetActiveScene().name;
        }

    }

    public enum SceneNames
    {
        MainGame,
        BossI,
        Start,
        Restart,
        Victory,
    }
}