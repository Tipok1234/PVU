using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.UI;
using Assets.Scripts.DataSo;
using Assets.Scripts.UIManager;
using Assets.Scripts.Enums;
using TMPro;
using System;

namespace Assets.Scripts.Managers
{
    public class CalendarManager : BaseWindow
    {
        [SerializeField] private RewardUI _rewardUIPrefab;
        [SerializeField] private Transform _spawnRewardPrefabUI;

        [SerializeField] private Canvas _rewardDailyCanvas;

        [SerializeField] private RewardDailySO _rewardDailySOs;

        private int _index = 1;
        private DataManager _dataManager;

        private List<RewardUI> _rewardUIItems = new List<RewardUI>();

        private bool _isReward = false;

        private void Start()
        {
            //_rewardDailyCanvas.enabled = false;

            _dataManager = FindObjectOfType<DataManager>();

            int rewardIndex = PlayerPrefs.GetInt("CalendarIndex", 0);

            if (rewardIndex >= _rewardDailySOs.RewardDailies.Length)
            {
                rewardIndex = 0;
            }


            for (int i = 0; i < _rewardDailySOs.RewardDailies.Length; i++)
            {
                RewardUI rewardUI = Instantiate(_rewardUIPrefab, _spawnRewardPrefabUI);

                rewardUI.Setup(_rewardDailySOs.RewardDailies[i].CurrenctCurrencyDaily, _index++);

                rewardUI.CollectRewardAction += OnCollectReward;

                _rewardUIItems.Add(rewardUI);

                if (i < rewardIndex)
                {
                    _rewardUIItems[i].ReceivedReward();
                }
                else if (i == rewardIndex)
                {

                    var dateString = PlayerPrefs.GetString("Reward");

                    if (string.IsNullOrEmpty(dateString))
                    {
                        _rewardDailyCanvas.enabled = true;
                        _isReward = true;
                    }
                    else
                    {
                        TimeSpan diff = DateTime.UtcNow - DateTime.Parse(dateString);

                        _rewardDailyCanvas.enabled = false;

                        if (diff.TotalHours > _rewardDailySOs.TimeReward)
                        {
                            _rewardDailyCanvas.enabled = true;
                            _isReward = true;

                        }
                    }

                    if (_isReward)
                    {
                        _rewardUIItems[i].OpenReward();
                    }
                    else
                    {
                        _rewardUIItems[i].LockkReward();
                    }

                }
                else
                {
                    _rewardUIItems[i].LockkReward();
                }
            }


        }

        public void OnCollectReward(float currency, RewardUI rewardUI)
        {
            int index = _rewardUIItems.IndexOf(rewardUI);

            if (index == -1)
            {
                Debug.LogError("INDEX EXEPTION");
                return;
            }

            Debug.LogError("OnCollectReward: " + index);

            index++;

            if (_isReward == true)
            {
                _rewardDailyCanvas.enabled = false;

                _dataManager.AddCurrency(currency, CurrencyType.HardCurrency);

                var dateTime = DateTime.UtcNow;

                PlayerPrefs.SetString("Reward", dateTime.ToString());
                PlayerPrefs.SetInt("CalendarIndex", index);

                rewardUI.ReceivedReward();
            }
            else
            {
                Debug.LogError("WRONG");
            }
        }
    }
}
