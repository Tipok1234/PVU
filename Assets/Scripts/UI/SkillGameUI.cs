using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Assets.Scripts.DataSo;
using Assets.Scripts.Enums;

namespace Assets.Scripts.UI
{
    public class SkillGameUI : MonoBehaviour
    {
        public event Action<SkillType, float> SkillSelectAction;
        public SkillDataSO SkillDataSO => _skillDataSO;

        [SerializeField] private SkillDataSO _skillDataSO;
        [SerializeField] private Button _skillButton;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private Image _fillImage;
        [SerializeField] private Color _noMoneyColor;
        [SerializeField] private Color _normalColor;

        private Sequence _sequence;
        private void Awake()
        {
            _skillButton.onClick.AddListener(CastSkillOnClick);
        }
        private void Start()
        {
            _priceText.text = _skillDataSO.PriceSkill.ToString();
        }

        private void CastSkillOnClick()
        {
            SkillSelectAction?.Invoke(_skillDataSO.SkillType,_skillDataSO.PriceSkill);
        }
        public void HightLightSkillUI(bool hightlight)
        {
            _fillImage.color = hightlight ? _normalColor : _noMoneyColor;
        }
        public void RechargeTimeSkill()
        {
            _sequence = DOTween.Sequence();

            _skillButton.enabled = false;
            _fillImage.fillAmount = 1;
            _sequence.Append(_fillImage.DOFillAmount(0f, _skillDataSO.CooldownSkill).SetEase(Ease.Linear).OnComplete(() => _skillButton.enabled = true));
        }

        private void OnDestroy()
        {
            _sequence?.Kill();
        }
    }
}
