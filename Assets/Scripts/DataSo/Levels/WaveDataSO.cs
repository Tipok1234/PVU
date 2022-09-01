using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Models;

namespace Assets.Scripts.DataSo
{
    [CreateAssetMenu(fileName = "Wave_", menuName = "GameSO/NewWave")]
    public class WaveDataSO : ScriptableObject
    {
        [SerializeField] private AttackUnit[] _enemyPrefab;

        private int _countInWave;
        private float _delayAfterWave;
    }
}
