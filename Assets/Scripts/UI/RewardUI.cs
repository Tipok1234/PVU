using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Enums;
using Assets.Scripts.AnimationsModel;
using TMPro;
using System;
using Assets.SimpleLocalization;

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

        private float _currentCurrency;
        private int _dayIndex;

        private void Awake()
        {
            _clickButton.onClick.AddListener(CollectRewardOnClick);
            LocalizationManager.LocalizationChanged += OnLanguageChanged;
        }
        public void Setup(float currentCurrency, int dayIndex)
        {
            _currentCurrency = currentCurrency;
            _currentDayCurrency.text = currentCurrency.ToString();
            _dayIndex = dayIndex;
            _dayText.text = LocalizationManager.Localize(LocalizationConst.Calendar + "Day", _dayIndex);
        }

        public void OpenReward()
        {
            _animationsModel.PlayAnimation();
            _lockReward.enabled = false;
            _received.enabled = false;
        }

        public void ReceivedReward()
        {
            _clickButton.enabled = false;
            _lockReward.enabled = false;
            _received.enabled = true;
        }

        public void LockkReward()
        {
            _clickButton.enabled = false;
            _lockReward.enabled = true;
            _received.enabled = false;
        }

        private void CollectRewardOnClick()
        {
            _animationsModel.ResetAnimation();
            CollectRewardAction?.Invoke(_currentCurrency, this);
        }
        private void OnLanguageChanged()
        {
            _dayText.text = LocalizationManager.Localize(LocalizationConst.Calendar + "Day", _dayIndex);
        }
    }
}
