using Assets.Scripts.Models;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.Managers
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] _unitPrefabs;

        [SerializeField] private float _spawnTime;
        private float _spawnUnitTime = 0;

        private IReadOnlyList<Transform> _spawnPositions;

        public void Setup(IReadOnlyList<Transform> enemyPoints)
        {
            _spawnPositions = enemyPoints;
            StartCoroutine(Setup());
        }

        private IEnumerator Setup()
        {
            while (true)
            {
                if (_spawnUnitTime < _spawnTime)
                {
                    yield return new WaitForSeconds(1f);
                    _spawnUnitTime++;
                }
                else
                {
                    var unitCount = Random.Range(0, _spawnPositions.Count);

                    for (int i = 0; i < unitCount; i++)
                    {
                        var randomPos =  Random.Range(0, _spawnPositions.Count);
                        var randomUnitPrefab = Random.Range(0, _unitPrefabs.Length);

                        Instantiate(_unitPrefabs[randomUnitPrefab], _spawnPositions[randomPos]);

                        yield return new WaitForSeconds(Random.Range(0.5f,1.5f));
                    }

                    _spawnUnitTime = 0;
                }
            }
        }
    }
}
