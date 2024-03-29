using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HomeDefense
{
    [RequireComponent(typeof(Enemy))]
    public class EnemyMovement : MonoBehaviour
    {
        //  Set enemy gameobject false on path end (reduce point)

        private GridManager _gridManager;
        private CurrencySystem _currencySystem;
        private AudioManager _audioManager;
        private UIManager _uiManager;
        private readonly List<Vector2> _enemyPath = new List<Vector2>();
        private Vector3 _cellSize;

        private void OnEnable()
        {   
            _gridManager = FindObjectOfType<GridManager>();
            _currencySystem = FindObjectOfType<CurrencySystem>();
            _audioManager = FindObjectOfType<AudioManager>();
            _uiManager = FindObjectOfType<UIManager>();

            _cellSize = _gridManager.Map.cellSize;    

            FindPath();  
            StartCoroutine(FollowPath());
        }

        private IEnumerator FollowPath()
        {
            foreach (Vector2 waypoint in _enemyPath)
            {
                int waypointIndex = _enemyPath.IndexOf(waypoint);

                Vector2 startPosition = waypoint;
                Vector2 endPosition = _enemyPath[waypointIndex + 1];

                float travelPercent = 0f;

                while (travelPercent < 1f)
                {
                    travelPercent += Time.deltaTime * _gridManager.GetTileSpeed(transform.position);
                    transform.position = Vector2.Lerp(startPosition, endPosition, travelPercent);
                    
                    yield return new WaitForEndOfFrame();
                }
            }
        }

        private void FindPath()
        {
            Vector2 enemyStartPosition = _gridManager.GetRandomSpawnPoint();
            
            _enemyPath.Add(enemyStartPosition);

            for (int i = 1; i < _gridManager.Map.size.x; i++)
            {
                float nextCell = i * _cellSize.x;
                _enemyPath.Add(new Vector2(enemyStartPosition.x + nextCell, enemyStartPosition.y));
            }
        }

        public void ResetPath()
        {
            _enemyPath.Clear();
            FindPath();
        }
      
        public void StealMoney()
        {
            _uiManager.ExecuteNotification("Stealed!");
            _currencySystem.WithdrawMoney(100);
            _audioManager.PlayStealSFX();
            ResetPath();
            gameObject.SetActive(false);            
        }
    }
}

