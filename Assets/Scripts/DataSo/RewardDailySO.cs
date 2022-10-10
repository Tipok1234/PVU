using UnityEngine;
using Assets.Scripts.Enums;

namespace Assets.Scripts.DataSo
{
    [CreateAssetMenu(fileName = "RewardDaily", menuName = "ScriptableObjects/RewardDailyData", order = 3)]
    public class RewardDailySO : ScriptableObject
    {
        public RewardDaily[] RewardDailies => _rewardDailies;
        public float TimeReward => _timeReward;

        [SerializeField] private float _timeReward;
        [SerializeField] private RewardDaily[] _rewardDailies;
    }

    [System.Serializable]
    public class RewardDaily
    {
        public float CurrenctCurrencyDaily => _currentCurrencyDaily;
        public CurrencyType CurrencyType => _currencyType;

        [SerializeField] private float _currentCurrencyDaily;
        [SerializeField] private CurrencyType _currencyType;
    }
}
