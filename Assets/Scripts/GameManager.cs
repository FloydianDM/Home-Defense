using UnityEngine;
using UnityEngine.SceneManagement;

namespace HomeDefense
{
    public class GameManager : MonoBehaviour
    {   
        public static GameManager Instance;

        private const string GAME_MENU = "Scene_Menu";
        private const string GAME_START = "Scene_Game";
        private const string GAME_OVER = "Scene_GameOver";

        private void Awake()
        {
            ManageSingleton();
        }

        private void ManageSingleton()
        {
            if (Instance != null && Instance != this)
            {
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        public void StartGame()
        {
            SceneManager.LoadScene(GAME_START);
        }

        public void LoseGame()
        {
            SceneManager.LoadScene(GAME_OVER);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }  
}

