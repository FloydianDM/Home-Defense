using UnityEngine;

namespace HomeDefense
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private int _enemyReward = 50;
        [SerializeField] private int _enemyPenalty = 50;

        private CurrencySystem _currencySystem;

        private void Start()
        {
            _currencySystem = FindObjectOfType<CurrencySystem>();
        }

        public void AddReward()
        {
            if (_currencySystem == null)
            {
                return;
            }

            _currencySystem.DepositMoney(_enemyReward);
        }

        public void AddPenalty()
        {
            if (_currencySystem == null)
            {
                return;
            }

            _currencySystem.WithdrawMoney(_enemyPenalty);
        }
    }
}

