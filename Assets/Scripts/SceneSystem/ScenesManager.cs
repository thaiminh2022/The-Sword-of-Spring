using System;
using System.Linq;
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
        public static void SwitchScene(string name)
        {
            string[] availableNames = Enum.GetNames(typeof(SceneNames));
            if (availableNames.ToList().Contains(name))
            {
                SceneManager.LoadScene(name);
            }
            else
            {
                Debug.LogError("NO SCENE HAS THAT NAME WTF");
            }
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
            SwitchScene(SceneNames.RestartScene);
        }
        public static void ToVictoryScene()
        {
            SwitchScene(SceneNames.VictoryScene);
        }
        public static void ToBossIScene()
        {
            SwitchScene(SceneNames.BossI);
        }
        public static void ToMainGameScene()
        {
            SwitchScene(SceneNames.MainGame);
        }
        public static void ToStartScene()
        {
            SwitchScene(SceneNames.StartScene);
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
        StartScene,
        RestartScene,
        VictoryScene,
    }
}