using UnityEngine;

namespace HomeDefense
{
    public class CurrencySystem : MonoBehaviour
    {
        [SerializeField] private int _startingBalance = 50;

        public int CurrentBalance { get; private set; }
        private GameManager _gameManager;

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
        }

        public void WithdrawMoney(int amount)
        {
            CurrentBalance -= Mathf.Abs(amount);
        
            if (CurrentBalance <= 0)
            {
                _gameManager.LoseGame();
            }
        }
    }
    
}

