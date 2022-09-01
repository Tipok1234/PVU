using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.DataSo
{
    [CreateAssetMenu(fileName = "Level_", menuName = "GameSO/NewLevel")]
    public class LevelDataSO : ScriptableObject
    {
        [SerializeField] private WaveDataSO[] _waves;
        [SerializeField] private float _delayBeforeStartWaves;
    }
}