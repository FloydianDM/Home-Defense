using UnityEngine;
using UnityEngine.SceneManagement;

namespace HomeDefense
{
    public class GameManager : MonoBehaviour
    {   
        public static GameManager Instance;

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

        public void LoseGame()
        {
            SceneManager.LoadScene(0);
        }
    }  
}

