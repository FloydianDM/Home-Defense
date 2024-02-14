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
        public const string ENHANCED_LIFETIME_KEY = "Enhanced Life Time";
        private AdManager _adManager;

        private void Awake()
        {
            ManageSingleton();

            _adManager = GetComponent<AdManager>();
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

        public void ProcessGameOver()
        {
            if (PlayerPrefs.HasKey(ENHANCED_LIFETIME_KEY))
            {
                PlayerPrefs.DeleteKey(ENHANCED_LIFETIME_KEY);
            }

            _adManager.ShowAd(this);
            SceneManager.LoadScene(GAME_OVER);
        }

        public void QuitGame()
        {
            if (PlayerPrefs.HasKey(ENHANCED_LIFETIME_KEY))
            {
                PlayerPrefs.DeleteKey(ENHANCED_LIFETIME_KEY);
            }
            
            Application.Quit();
        }

        public void SetEnhancedLife(float addedTime)
        {
            PlayerPrefs.SetFloat(ENHANCED_LIFETIME_KEY, addedTime);
        }
    }  
}

