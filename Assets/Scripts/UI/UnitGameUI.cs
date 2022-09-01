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
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private Button _buyButton;
        [SerializeField] private Image _unitImage;

        public event Action<DefenceUnitType> BuyUnitAction;

        private DefenceUnitType _unitType;

        private void Awake()
        {
            _buyButton.onClick.AddListener(BuyButton);
        }
        public void Setup(UnitDataSo unitDataSo)
        {       
            _priceText.text = unitDataSo.Price.ToString();
            _unitImage.sprite = unitDataSo.UnitSprite;
            _unitType = unitDataSo.DefencUnitType;
        }

        public void BuyButton()
        {
            BuyUnitAction?.Invoke(_unitType);
        }
    }
}
