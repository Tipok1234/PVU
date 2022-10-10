using UnityEngine;

namespace Assets.Scripts.DataSo
{
    [CreateAssetMenu(fileName = "Level_", menuName = "GameSO/NewLevel")]
    public class LevelDataSO : ScriptableObject
    {
        public WaveDataSO[] Waves => _waves;
        public float DelayBeforeStartWaves => _delayBeforeStartWaves;

        [SerializeField] private WaveDataSO[] _waves;
        [SerializeField] private float _delayBeforeStartWaves;

        public int GetEnemy()
        {
            int enemyCountInLevel = 0;

            for (int i = 0; i < _waves.Length; i++)
            {
                for (int j = 0; j < _waves[i].WavesData.Length; j++)
                {
                    enemyCountInLevel += _waves[i].WavesData[j].CountInWave;
                }
            }
            return enemyCountInLevel;
        }
    }
}