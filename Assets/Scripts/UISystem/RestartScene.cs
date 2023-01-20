using UnityEngine;
using TheSwordOfSpring.SceneSystem;
using UnityEngine.UI;

namespace TheSwordOfSpring.UISystem
{
    public class RestartScene : MonoBehaviour
    {
        [SerializeField] Button menuButton;
        [SerializeField] Button restartButton;

        [SerializeField] CanvasGroup holder;

        private void Start()
        {
            holder.alpha = 0;
            holder.LeanAlpha(1, 1f).setEaseInSine();

            menuButton.onClick.AddListener(() =>
            {
                ScenesManager.ToStartScene();
            });
            restartButton.onClick.AddListener(() =>
            {
                ScenesManager.ToBossIScene();
            });

        }

    }
}