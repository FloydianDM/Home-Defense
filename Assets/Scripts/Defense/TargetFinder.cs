using UnityEngine;

namespace HomeDefense
{
    public class TargetFinder : MonoBehaviour
    {
        [SerializeField] private Transform _shooter;
        [SerializeField] private float _towerRange;
        [SerializeField] private GameObject _ammo;

        private Transform _target;
        private ParticleSystem _ammoParticles;

        private void Start()
        {
            _ammoParticles = _ammo.GetComponent<ParticleSystem>();   
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
            float maxDistance = Mathf.Infinity;

            foreach (Enemy enemy in enemies)
            {
                float targetDistance = Vector3.Distance(enemy.transform.position, transform.position);

                if (targetDistance < maxDistance)
                {
                    closestTarget = enemy.transform;
                    maxDistance = targetDistance;
                }
            }

            _target = closestTarget;
        }

        private void AimTarget()
        {
            if (_target == null)
            {
                return;
            }
            
            float targetDistance = Vector3.Distance(transform.position, _target.position);

            ToggleAttack(targetDistance <= _towerRange);

            _shooter.LookAt(_target);
        }

        private void ToggleAttack(bool isInRange)
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
    }    
}

