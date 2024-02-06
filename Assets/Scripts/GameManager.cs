using UnityEngine;
using UnityEngine.SceneManagement;

namespace HomeDefense
{
    public class GameManager : MonoBehaviour
    {   
        public static GameManager Instance;

        private const string GameMenu = "Scene_Menu";
        private const string GameStart = "Scene_Game";

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
            SceneManager.LoadScene(GameStart);
        }

        public void LoseGame()
        {
            SceneManager.LoadScene(GameMenu);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }  
}

