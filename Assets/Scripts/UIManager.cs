using TMPro;
using UnityEngine;

namespace HomeDefense
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _goldText;
        [SerializeField] private TextMeshProUGUI _highScoreText;

        private CurrencySystem _currencySystem;

        private void Start()
        {
            _currencySystem = FindObjectOfType<CurrencySystem>();
            
            ChangeHighScore();
            ChangeGoldText();
            _currencySystem.OnCurrencyChange += ChangeGoldText;
        }

        private void ChangeGoldText()
        {
            if (_goldText == null)
            {
                return;
            }

            _goldText.text = $"GOLD: {_currencySystem.CurrentBalance}";
        }

        private void ChangeHighScore()
        {
            if (_highScoreText == null)
            {
                return;
            }

            _highScoreText.text = $"High Score: {PlayerPrefs.GetInt(CurrencySystem.MAX_SCORE_KEY)}";
        }
    }
}

