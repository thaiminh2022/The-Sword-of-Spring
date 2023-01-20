using UnityEngine;
using TMPro;
using TheSwordOfSpring.Misc;
namespace TheSwordOfSpring.CharacterSystem
{

    public class SetPlayerUIName : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI playerName;


        private void Start()
        {
            playerName.SetText(GameManager.Instance?.GetPlayerName() ?? "Thaiminh022");
        }
    }
}