using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Assets.Scripts.DataSo;
using Assets.Scripts.Enums;
using Assets.Scripts.Managers;
using DG.Tweening;

namespace Assets.Scripts.UIManager
{
    public class UnitGameUI : MonoBehaviour
    {
        public DefenceUnitType UnitType => _unitType;
        public int UnitPrice => _unitPrice;

        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private Button _buyButton;
        [SerializeField] private Image _unitImage;
        [SerializeField] private Image _fillImage;
        [SerializeField] private Color _noMoneyColor;
        [SerializeField] private Color _normalColor;

        public event Action<DefenceUnitType> BuyUnitAction;

        private DefenceUnitType _unitType;
        private int _unitPrice;
        private float _placeCooldown;
        private void Awake()
        {
            _buyButton.onClick.AddListener(BuyButton);
        }

        //public void Setup(DataManager dataManager)
        //{
        //    _placeCooldown = ((float)CharacteristicUnitType.Recharge);
        //    _unitPrice = ((int)CharacteristicUnitType.Price);
        //    _priceText.text = CharacteristicUnitType.Price.ToString();
        //    _unitImage.sprite =
        //    _unitType = unitDataSo.DefencUnitType;
        //}

        public void Setup(UnitDataSo unitDataSo)
        {
            _placeCooldown = unitDataSo.PlaceCooldown;
            _unitPrice = unitDataSo.Price;
            _priceText.text = unitDataSo.Price.ToString();
            _unitImage.sprite = unitDataSo.UnitSprite;
            _unitType = unitDataSo.DefencUnitType;
        }

        public void HightLight(bool hightlight)
        {
            _unitImage.color = hightlight ? _normalColor : _noMoneyColor;
        }
        public void BuyButton()
        {
            BuyUnitAction?.Invoke(_unitType);
        }

        public void RechargeTime()
        {
            _buyButton.enabled = false;
            _fillImage.fillAmount = 1;
            _fillImage.DOFillAmount(0f, _placeCooldown).SetEase(Ease.Linear).OnComplete(()=> _buyButton.enabled = true);
        }

    }
}
