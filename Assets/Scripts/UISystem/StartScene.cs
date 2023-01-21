using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TheSwordOfSpring.Misc;
using TheSwordOfSpring.AudioSystem;

namespace TheSwordOfSpring.UISystem
{

    public class StartScene : MonoBehaviour
    {
        [SerializeField] Button playerButton;
        [SerializeField] TMP_InputField inputField;
        private void Start()
        {
            AudioManager.Instance.StopAllMusic();
            AudioManager.Instance.PlayMusic("LoveSong", true);


            playerButton.onClick.AddListener(() =>
            {
                GameManager.Instance?.SetPlayerName(inputField.text);
            });
        }



    }
}