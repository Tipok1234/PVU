using Assets.Scripts.Models;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private Transform[] _spawnPositions;
        [SerializeField] private GameObject[] _unitPrefabs;

        [SerializeField] private float _spawnTime;
        private float _spawnUnitTime = 0;
        


        private void Start()
        {
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
                    var unitCount = Random.Range(0, _spawnPositions.Length);

                    for (int i = 0; i < unitCount; i++)
                    {
                        var randomPos =  Random.Range(0, _spawnPositions.Length);
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
