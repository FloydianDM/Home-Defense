using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HomeDefense
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _goldText;
        [SerializeField] private TextMeshProUGUI _highScoreText;
        [SerializeField] private GameObject _notificationPanel;
        [SerializeField] private TextMeshProUGUI _notificationText;

        private CurrencySystem _currencySystem;
        private PlayerMovement _playerMovement;

        private void Start()
        {
            _currencySystem = FindObjectOfType<CurrencySystem>();
            _playerMovement = FindObjectOfType<PlayerMovement>();

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

        public void SelectMeleeButton()
        {
            _playerMovement.DefenseType = DefenseType.melee;

            StartCoroutine(ShowNotificationPanel("Melee selected"));
        }

        public void SelectRangedButton()
        {
            _playerMovement.DefenseType = DefenseType.ranged;

            StartCoroutine(ShowNotificationPanel("Ranged selected"));
        }

        public IEnumerator ShowNotificationPanel(string text)
        {
            _notificationPanel.SetActive(true);
            _notificationText.text = text;

            yield return new WaitForSeconds(1f);

            _notificationPanel.SetActive(false);
        }

        public void ExecuteNotification(string text)
        {
            StopAllCoroutines();
            StartCoroutine(ShowNotificationPanel(text));
        }

        public void ShowTowerHealth(Image timeBarImage, float life, float timer)
        {
            float fillRatio = timer / life;

            timeBarImage.fillAmount = fillRatio;
        }
    }
}

