using System;
using System.Collections;
using UnityEngine;

namespace HomeDefense
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private GameObject _enemy;
        [SerializeField] [Range(0, 50)] private int _poolSize = 5;
        [SerializeField] [Range(0.1f, 20f)] private float _spawnTime = 1f;

        private GameObject[] _pool;

        private void Start()
        {
            PopulatePool();
            StartCoroutine(SpawnEnemies());
        }

        private void PopulatePool()
        {
            _pool = new GameObject[_poolSize];

            for (int i = 0; i < _pool.Length; i++)
            {
                _pool[i] = Instantiate(_enemy, transform);
                _pool[i].SetActive(false); 
            }
        }

        private void EnableObjectInPool()
        {
            for (int i = 0; i < _pool.Length; i++)
            {
                if (_pool[i].activeInHierarchy == false)
                {
                    _pool[i].SetActive(true);
                    
                    return;
                }
            }
        }

        private IEnumerator SpawnEnemies()
        {
            while (true)
            {
                EnableObjectInPool();

                yield return new WaitForSeconds(_spawnTime);
            }
        }
    }
}
