using UnityEngine;
using System.Collections.Generic;
using TheSwordOfSpring.SceneSystem;
using TheSwordOfSpring.TimeSystem;
using TheSwordOfSpring.UISystem;

namespace TheSwordOfSpring.UISystem
{
    public class UIHelper : MonoBehaviour
    {

        public void DisableGameObject(GameObject gameObject)
        {
            gameObject.SetActive(false);
        }
        public void EnableGameObject(GameObject gameObject)
        {
            gameObject.SetActive(true);
        }

        public void SwitchScene(string name)
        {
            ScenesManager.SwitchScene(name);
        }

        public void QuitGame()
        {
            ScenesManager.ExitApplication();
        }

        public void RestartScene()
        {
            ScenesManager.RestartCurrentScene();
        }

        public void ResumeScene()
        {
            GameTimeManager.Resume();
            UIManager.UsePlayerMode();
        }
        public void PauseScene()
        {
            GameTimeManager.Pause();
        }

    }
}