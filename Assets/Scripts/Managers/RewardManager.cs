using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.UI;
using Assets.Scripts.DataSo;

namespace Assets.Scripts.Managers
{
    public class RewardManager : MonoBehaviour
    {
        [SerializeField] private RewardUI _rewardUIPrefab;
        [SerializeField] private Transform _spawnRewardPrefabUI;

        [SerializeField] private RewardDailySO[] _rewardDailySOs;

        private void Start()
        {
            for (int i = 0; i < _rewardDailySOs.Length; i++)
            {
                for (int j = 0; j < _rewardDailySOs[i].RewardDailies.Length; j++)
                {
                    RewardUI rewardUI = Instantiate(_rewardUIPrefab, _spawnRewardPrefabUI);

                   // rewardUI.Setup(_rewardDailySOs[i].RewardDailies[j].CurrenctCurrencyDaily, i + 1);
                }
            }
        }
    }
}
