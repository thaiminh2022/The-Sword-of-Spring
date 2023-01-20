using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TheSwordOfSpring.Misc;

namespace TheSwordOfSpring.UISystem
{

    public class StartScene : MonoBehaviour
    {
        [SerializeField] Button playerButton;
        [SerializeField] TMP_InputField inputField;
        private void Start()
        {
            playerButton.onClick.AddListener(() =>
            {
                GameManager.Instance?.SetPlayerName(inputField.text);
            });
        }



    }
}