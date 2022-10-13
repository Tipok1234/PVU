using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.UI;
using Assets.Scripts.DataSo;
using Assets.Scripts.Enums;
using System;

namespace Assets.Scripts.Managers
{
    public class CalendarManager : BaseWindow
    {
        [SerializeField] private RewardUI _rewardUIPrefab;
        [SerializeField] private Transform _spawnRewardPrefabUI;

        [SerializeField] private Canvas _rewardDailyCanvas;

        [SerializeField] private RewardDailySO _rewardDailySOs;

        private int _index;
        private DataManager _dataManager;

        private List<RewardUI> _rewardUIItems = new List<RewardUI>();

        private bool _isReward = false;

        private void Start()
        {
            _dataManager = FindObjectOfType<DataManager>();

            if (_dataManager.CalendarIndex >= _rewardDailySOs.RewardDailies.Length)
            {
                _dataManager.SaveCalendarIndex(0);
            }


            for (int i = 0; i < _rewardDailySOs.RewardDailies.Length; i++)
            {
                RewardUI rewardUI = Instantiate(_rewardUIPrefab, _spawnRewardPrefabUI);

                rewardUI.Setup(_rewardDailySOs.RewardDailies[i].CurrenctCurrencyDaily, i);

                rewardUI.CollectRewardAction += OnCollectReward;

                _rewardUIItems.Add(rewardUI);

                if (i < _dataManager.CalendarIndex)
                {
                    _rewardUIItems[i].ReceivedReward();
                }
                else if (i == _dataManager.CalendarIndex)
                {

                    if (string.IsNullOrEmpty(_dataManager.DateString))
                    {
                        _rewardDailyCanvas.enabled = true;
                        _isReward = true;
                    }
                    else
                    {
                        TimeSpan diff = DateTime.UtcNow - DateTime.Parse(_dataManager.DateString);

                        _rewardDailyCanvas.enabled = false;

                        if (diff.TotalSeconds >  20)// _rewardDailySOs.TimeReward)
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
            _index = _rewardUIItems.IndexOf(rewardUI);

            Debug.LogError("IDNEX: " + _index);


            if (_index == -1)
            {
                Debug.LogError("INDEX EXEPTION");
                return;
            }

            if (_isReward == true)
            {
                _index++;

                _rewardDailyCanvas.enabled = false;

                _dataManager.AddCurrency(currency, CurrencyType.HardCurrency);

                var dateTime = DateTime.UtcNow;

                _dataManager.SaveDateTimeCalendarReward(dateTime);

                _dataManager.SaveCalendarIndex(_index);

                rewardUI.ReceivedReward();
            }
            else
            {
                Debug.LogError("WRONG");
            }
        }
    }
}
