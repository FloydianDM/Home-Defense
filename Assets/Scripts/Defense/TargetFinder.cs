using System;
using UnityEngine;

namespace HomeDefense
{
    public enum DefenseType
    {
        ranged,
        melee
    }

    public class TargetFinder : MonoBehaviour
    {
        [SerializeField] private Transform _hitter;
        [SerializeField] private float _towerRange;
        [SerializeField] private GameObject _ammo;

        private Transform _target;
        private Enemy _enemy;
        private ParticleSystem _ammoParticles;
        private PlayerMovement _playerMovement;
        private DefenseType _defenseType;   // storing enum reference for prevent changing correct enum type for each Tower object

        private void Start()
        {
            _playerMovement = FindObjectOfType<PlayerMovement>();
            _defenseType = _playerMovement.DefenseType;        

            if (_ammo != null)
            {
                Debug.Log("Ranged selected");
                SelectRangedAttack(true);
            }
            else
            {
                Debug.Log("Melee selected");
                SelectRangedAttack(false);
            }
        }

        private void SelectRangedAttack(bool isRanged)
        {
            if (isRanged)
            {
                _ammoParticles = _ammo.GetComponent<ParticleSystem>();
            }
            else
            {
                _ammoParticles = null;
            }
        }

        private void Update()
        {
            FindTarget();
            AimTarget();
        }

        private void FindTarget()
        {
            //  Find every enemy target in the scene and compare distances of them, select closest one

            Enemy[] enemies = FindObjectsOfType<Enemy>();
            Transform closestTarget = null;
            Enemy closestEnemy = null;
            float maxDistance = Mathf.Infinity;

            foreach (Enemy enemy in enemies)
            {
                float targetDistance = Vector3.Distance(enemy.transform.position, transform.position);

                if (targetDistance < maxDistance)
                {
                    closestTarget = enemy.transform;
                    maxDistance = targetDistance;

                    closestEnemy = enemy;
                }
            }

            _target = closestTarget;
            _enemy = closestEnemy;
        }

        private void AimTarget()
        {
            if (_target == null)
            {
                return;
            }
            
            float targetDistance = Vector3.Distance(transform.position, _target.position);
            ToggleAttack(targetDistance <= _towerRange);

            _hitter.LookAt(_target);
        }

        private void ToggleAttack(bool isInRange)
        {
            switch (_defenseType)
            {
                case DefenseType.ranged:
                    ExecuteShoot(isInRange);
                    break;

                case DefenseType.melee:
                    ExecuteMeleeAttack(isInRange);
                    break;                
            }
        }

        private void ExecuteShoot(bool isInRange)
        {
            var ammoEmission = _ammoParticles.emission;
            
            if (!isInRange)
            {
                ammoEmission.enabled = false;
            }
            else
            {
                ammoEmission.enabled = true;
            }
        }

        private void ExecuteMeleeAttack(bool isInRange)
        {
            if (!isInRange)
            {
                return;
            }
            else
            {
                _enemy.GetComponent<EnemyHit>().ProcessHit();
            }
        }
    }    
}

