using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Models;
using Assets.Scripts.Enums;

namespace Assets.Scripts.DataSo
{
    [CreateAssetMenu(fileName = "Wave_", menuName = "GameSO/NewWave")]
    public class WaveDataSO : ScriptableObject
    {
        public float DelayBetweenUnits => _delayBetweenUnits;
        public float DelayAfterWave => _delayAfterWave;
        public WaveData[] WavesData => _waveData;

        [SerializeField] private WaveData[] _waveData;
        [SerializeField] private float _delayBetweenUnits;
        [SerializeField] private float _delayAfterWave;
    }

    [System.Serializable]
    public class WaveData
    {
        public int CountInWave => _countInWave;
        public AttackUnitType AttackUnit => _attackUnit;

        [SerializeField] private AttackUnitType _attackUnit;
        [SerializeField] private int _countInWave;
    }
}
