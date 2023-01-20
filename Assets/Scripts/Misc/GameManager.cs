using UnityEngine;
namespace TheSwordOfSpring.Misc
{

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        private string PlayerName;


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);

        }
        public string GetPlayerName() => PlayerName;
        public void SetPlayerName(string newName) => PlayerName = newName;




    }
}