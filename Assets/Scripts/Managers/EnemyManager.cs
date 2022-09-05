using Assets.Scripts.Models;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.DataSo;
using System;
using Assets.Scripts.Grids;
using Assets.Scripts.UIManager;

namespace Assets.Scripts.Managers
{
    public class EnemyManager : MonoBehaviour
    {
        public event Action LevelCompletedAction;
        [SerializeField] private AttackUnit[] _unitPrefabs;   

        private float _spawnUnitTime = 0;

        private IReadOnlyList<Transform> _spawnPositions;

        private LevelDataSO _levelDataSo;
        private int _enemyCountInLevel;
        public void Setup(IReadOnlyList<Transform> enemyPoints, LevelDataSO levelDataSo)
        {
            _levelDataSo = levelDataSo;
            _spawnPositions = enemyPoints;
            _enemyCountInLevel = levelDataSo.GetEnemy();
            StartCoroutine(StartLevelCoroutine());
        }
        private IEnumerator StartLevelCoroutine()
        {
            yield return new WaitForSeconds(_levelDataSo.DelayBeforeStartWaves);

            int waveIndex = 0;
            _spawnUnitTime = _levelDataSo.Waves[waveIndex].DelayBetweenUnits;

            while (true)
            {
                if(waveIndex >= _levelDataSo.Waves.Length)
                {
                    yield break;
                }
                        

                if (_spawnUnitTime < _levelDataSo.Waves[waveIndex].DelayBetweenUnits)
                {
                    yield return new WaitForSeconds(1f);
                    _spawnUnitTime++;
                }
                else
                {
                    for (int i = 0; i < _levelDataSo.Waves[waveIndex].WavesData.Length; i++)
                    {
                        var attackUnitType = _levelDataSo.Waves[waveIndex].WavesData[i].AttackUnit;
                        AttackUnit unitPrefab = null;

                        for (int k = 0; k < _unitPrefabs.Length; k++)
                        {
                            if (_unitPrefabs[k].AttackUnitType == attackUnitType)
                            {
                                unitPrefab = _unitPrefabs[k];
                            }
                        }

                        for (int j = 0; j < _levelDataSo.Waves[waveIndex].WavesData[i].CountInWave; j++)
                        {
                            var randomPos = UnityEngine.Random.Range(0, _spawnPositions.Count);
                            Instantiate(unitPrefab, _spawnPositions[randomPos]).UnitDeadAction += OnUnitDead;

                            yield return new WaitForSeconds(_levelDataSo.Waves[waveIndex].DelayBetweenUnits);
                        }            
                    }

                    waveIndex++;
                    _spawnUnitTime = 0;
                }
            }
        }
        private void OnUnitDead()
        {
            _enemyCountInLevel--;

            if(_enemyCountInLevel == 0)
            {
                LevelCompletedAction?.Invoke();
                Debug.LogError("Level Win");
            }
        }
    }
}
