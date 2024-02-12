using System;
using UnityEngine;

namespace HomeDefense
{
    public class CurrencySystem : MonoBehaviour
    {
        [SerializeField] private int _startingBalance = 50;

        public int CurrentBalance { get; private set; }
        private GameManager _gameManager;
        public const string MAX_SCORE_KEY = "Max Score";
        public event Action OnCurrencyChange;

        private void Awake()
        {
            CurrentBalance = _startingBalance;
        }
        
        private void Start()
        {
            _gameManager = FindObjectOfType<GameManager>();
        }

        public void DepositMoney(int amount)
        {
            CurrentBalance += Mathf.Abs(amount);

            OnCurrencyChange?.Invoke();
        }

        public void WithdrawMoney(int amount)
        {
            CurrentBalance -= Mathf.Abs(amount);
        
            OnCurrencyChange?.Invoke();

            if (CurrentBalance <= 0)
            {
                _gameManager.LoseGame();
            }
        }

        private void OnDestroy()
        {
            int currentHighScore = PlayerPrefs.GetInt(MAX_SCORE_KEY, 0);

            if (CurrentBalance > currentHighScore)
            {
                PlayerPrefs.SetInt(MAX_SCORE_KEY, CurrentBalance);
            }
        }
    }    
}

