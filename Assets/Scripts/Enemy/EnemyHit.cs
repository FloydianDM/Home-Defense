using UnityEngine;

namespace HomeDefense
{
    [RequireComponent(typeof(Enemy))]
    public class EnemyHit : MonoBehaviour
    {
        [SerializeField] private int _maxHitPoint = 5;
        [SerializeField] private int _difficultyMultiplier = 1;

        private Enemy _enemy;
        private int _enemyHitPoints;

        private void OnEnable()
        {
            _enemyHitPoints = _maxHitPoint;
        }

        private void Start()
        {
            _enemy = GetComponent<Enemy>();   
        }

        private void OnParticleCollision(GameObject other)
        {
            if (!other.CompareTag("Ammo"))
            {
                return;
            }

            ProcessHit();
        }

        public void ProcessHit()
        {
            _enemyHitPoints--;

            if (_enemyHitPoints <= 0)
            {
                gameObject.SetActive(false);
                _maxHitPoint += _difficultyMultiplier;
                _enemy.AddReward();
            }
        }
    }
}

