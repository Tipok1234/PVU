using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.DataSo;
using TMPro;

namespace Assets.Scripts.UI
{
    public class RewardUI : MonoBehaviour
    {
        [SerializeField] private Image _rewardImage;
        [SerializeField] private Button _clickButton;

        [SerializeField] private TMP_Text _dayText;
        [SerializeField] private TMP_Text _currentDayCurrency;

        private float _dailyCountCurrency;
        private void Awake()
        {
            _clickButton.onClick.AddListener(CollectReward);
        }
        public void Setup(RewardDailySO[] rewardDailySO, int dayIndex)
        {
            for (int i = 0; i < rewardDailySO.Length; i++)
            {
                for (int j = 0; j < rewardDailySO[i].RewardDailies.Length; j++)
                {
                    _currentDayCurrency.text = rewardDailySO[i].RewardDailies[j].CurrenctCurrencyDaily.ToString();
                    _dayText.text = "DAY " + dayIndex.ToString();
                }
            }
        }

        public void CollectReward()
        {

        }
    }
}
