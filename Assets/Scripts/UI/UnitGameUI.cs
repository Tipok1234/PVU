using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Assets.Scripts.DataSo;
using Assets.Scripts.Enums;

namespace Assets.Scripts.UIManager
{
    public class UnitGameUI : MonoBehaviour
    {
        public int UnitPrice => _unitPrice;

        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private Button _buyButton;
        [SerializeField] private Image _unitImage;
        [SerializeField] private Color _noMoneyColor;
        [SerializeField] private Color _normalColor;

        public event Action<DefenceUnitType> BuyUnitAction;

        private DefenceUnitType _unitType;
        private int _unitPrice;
        private void Awake()
        {
            _buyButton.onClick.AddListener(BuyButton);
        }
        public void Setup(UnitDataSo unitDataSo)
        {
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
    }
}
