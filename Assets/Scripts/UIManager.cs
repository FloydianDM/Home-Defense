using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace HomeDefense
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _goldText;

        private CurrencySystem _currencySystem;

        private void Start()
        {
            _currencySystem = FindObjectOfType<CurrencySystem>();
            
            ChangeGoldText();
            _currencySystem.OnCurrencyChange += ChangeGoldText;
        }

        private void ChangeGoldText()
        {
            _goldText.text = $"GOLD: {_currencySystem.CurrentBalance}";
        }
    }
}

