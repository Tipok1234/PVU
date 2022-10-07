using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.DataSo;
using Assets.Scripts.Enums;
using Assets.Scripts.Managers;
using Assets.Scripts.AnimationsModel;
using TMPro;
using System;

namespace Assets.Scripts.UI
{
    public class RewardUI : MonoBehaviour
    {
        public event Action<float, RewardUI> CollectRewardAction;
        public Image LockReward => _lockReward;
        public Image Received => _received;

        [SerializeField] private Image _rewardImage;
        [SerializeField] private Button _clickButton;

        [SerializeField] private TMP_Text _dayText;
        [SerializeField] private TMP_Text _currentDayCurrency;

        [SerializeField] private Image _lockReward;
        [SerializeField] private Image _received;
        [SerializeField] private AnimationModel _animationsModel;


        private RewardType _rewardType;

        private float _currentCurrency;

        private void Awake()
        {
            _clickButton.onClick.AddListener(CollectRewardOnClick);
        }
        public void Setup(float currentCurrency, int dayIndex)
        {
            _currentCurrency = currentCurrency;
            _currentDayCurrency.text = currentCurrency.ToString();
            _dayText.text = "DAY " + dayIndex.ToString();
        }

        public void OpenReward()
        {
            _animationsModel.PlayAnimation();
            _lockReward.enabled = false;
            _received.enabled = false;
            _rewardType = RewardType.Open_Type;
        }

        public void ReceivedReward()
        {
            _clickButton.enabled = false;
            _lockReward.enabled = false;
            _received.enabled = true;
            _rewardType = RewardType.Received_Type;
        }

        public void LockkReward()
        {
            _clickButton.enabled = false;
            _lockReward.enabled = true;
            _received.enabled = false;
            _rewardType = RewardType.Lock_Type;
        }

        public void CollectRewardOnClick()
        {
            _animationsModel.ResetAnimation();
            CollectRewardAction?.Invoke(_currentCurrency, this);
        }
    }
}
