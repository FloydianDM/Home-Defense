using UnityEngine;
using UnityEngine.UI;

namespace HomeDefense
{
    public class Defense : MonoBehaviour
    {
        [SerializeField] private int _defenseCost = 10;
        [SerializeField] private float _lifeTime = 30f;
        [SerializeField] private Image _lifebarImage;

        private float _timer = 0;
        private GridManager _gridManager;
        private UIManager _uiManager;

        private void Start()
        {
            _gridManager = FindObjectOfType<GridManager>();
            _uiManager = FindObjectOfType<UIManager>();

            CheckEnhancedTime();

            Debug.Log(_lifeTime);
        }

        private void Update()
        {
            SetTimer();
        }

        public bool CanCreateDefense(Defense defense, Vector3 createPosition)
        {
            CurrencySystem currencySystem = FindObjectOfType<CurrencySystem>();

            if (currencySystem == null)
            {
                return false;
            }

            if (currencySystem.CurrentBalance >= _defenseCost)
            {
                currencySystem.WithdrawMoney(_defenseCost);
                Instantiate(defense, createPosition, Quaternion.identity);

                return true;
            }

            return false;
        }

        private void SetTimer()
        {
            _timer += Time.deltaTime;
            
            if (_timer >= _lifeTime)
            {
                Vector3Int tilePosition =  _gridManager.GetTilePosition(transform.position);
                _gridManager.PlaceableCoordinatesDict[tilePosition] = true;

                Destroy(gameObject);
            
                return;
            }

            _uiManager.ShowTowerHealth(_lifebarImage, _lifeTime, _timer);
        }

        private void CheckEnhancedTime()
        {
            _lifeTime = PlayerPrefs.GetFloat(GameManager.ENHANCED_LIFETIME_KEY, _lifeTime);
        }
    }   
}

