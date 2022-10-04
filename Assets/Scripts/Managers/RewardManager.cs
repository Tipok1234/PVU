using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.UI;
using Assets.Scripts.DataSo;
using Assets.Scripts.UIManager;
using System;

namespace Assets.Scripts.Managers
{
    public class RewardManager : MonoBehaviour
    {
        [SerializeField] private RewardUI _rewardUIPrefab;
        [SerializeField] private Transform _spawnRewardPrefabUI;

        [SerializeField] private Canvas _rewardDailyCanvas;

        [SerializeField] private RewardDailySO _rewardDailySOs;
        [SerializeField] private ShopWindow _shopWindow;

        private int _index = 1;
        private DataManager _dataManager;

        private List<RewardUI> _rewardUIPrefabs = new List<RewardUI>();

        private int _isCountStreak = 30;

        private bool _isReward = false;

        private void Start()
        {
            _dataManager = FindObjectOfType<DataManager>();

            for (int i = 0; i < _rewardDailySOs.RewardDailies.Length; i++)
            {
                RewardUI rewardUI = Instantiate(_rewardUIPrefab, _spawnRewardPrefabUI);

                rewardUI.Setup(_rewardDailySOs.RewardDailies[i].CurrenctCurrencyDaily, _index++);

                rewardUI.CollectRewardAction += OnCollectReward;

                _rewardUIPrefabs.Add(rewardUI);
            }



            // for (int i = 0; i < _rewardUIPrefabs.Count; i++)
            //  {
            var dateString = PlayerPrefs.GetString("Reward");

            if (string.IsNullOrEmpty(dateString))
            {
                _isReward = true;
                Debug.LogError("TRUE");
                _rewardUIPrefabs[0].OpenReward(Enums.RewardType.Open_Type);
                _rewardUIPrefabs[1].ReceivedReward(Enums.RewardType.Received_Type);
                _rewardUIPrefabs[2].LockkReward(Enums.RewardType.Lock_Type);
            }
            else
            {
                _rewardUIPrefabs[0].Received.enabled = true;
                _rewardUIPrefabs[0].LockReward.enabled = false;
                TimeSpan diff = DateTime.UtcNow - DateTime.Parse(dateString);
                Debug.LogError("FALSE");
                //_rewardUIPrefabs[i].LockReward.enabled = true;
                //Debug.LogError($"UTC NOW: {DateTime.UtcNow} - {DateTime.Parse(dateString)} = {diff.ToString()}");

                if (diff.TotalSeconds > 30)
                {
                    _isReward = true;
                }
            }
            //  }
        }

        public void OnCollectReward(float currency)
        {
            if (_isReward == true)
            {
                _rewardDailyCanvas.enabled = false;

                _dataManager.AddCurrency(currency, Enums.CurrencyType.HardCurrency);

                _shopWindow.UpdateCurrency();


                var dateTime = DateTime.UtcNow;

                // Debug.LogError(" TO UNIVERSAL TIME : " + dateTime.ToUniversalTime().ToString());

                PlayerPrefs.SetString("Reward", dateTime.ToString());

            }
            else
            {
                Debug.LogError("WRONG");
            }

        }
    }
}
