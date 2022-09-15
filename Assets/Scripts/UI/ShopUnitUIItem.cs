using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using Assets.Scripts.Enums;
using Assets.Scripts.DataSo;


namespace Assets.Scripts.UIManager
{
    public class ShopUnitUIItem : MonoBehaviour
    {
        public event Action<DefenceUnitType> SelectUnitAction;
        public DefenceUnitType DefenceUnitType => _defenceUnitType;
        public bool IsOpen => _isOpen;
        public Sprite UnitSprite => _unitImage.sprite;

        [SerializeField] private Image _unitImage;
        [SerializeField] private Image _lockImage;
        [SerializeField] private Image _hardCurrencyImage;
        [SerializeField] private Image _softCurrencyImage;


        [SerializeField] private TMP_Text _unlockUnitText;
        [SerializeField] private TMP_Text _priceUpgradeUnitText;
        [SerializeField] private Button _selectUnitButton;

        private DefenceUnitType _defenceUnitType;
        private bool _isOpen = false;
        private void Awake()
        {
            _selectUnitButton.onClick.AddListener(SelectUnitUI);
        }

        public void Setup(UnitDataSo unitDataSO)
        {
            _defenceUnitType = unitDataSO.DefencUnitType;
            _unitImage.sprite = unitDataSO.UnitSprite;


            if (unitDataSO.IsOpen)
            {
                OpenUnit();
            }
            else
            {
                CloseUnit();
            }

        }

        public void UpdatePriceText(int price, CurrencyType currencyType)
        {
            switch (currencyType)
            {
                case CurrencyType.HardCurrency:
                    _unlockUnitText.text = price.ToString();
                    break;


                case CurrencyType.SoftCurrency:
                    _priceUpgradeUnitText.text = price.ToString();
                    break;
            }
        }

        private void CloseUnit()
        {
            _hardCurrencyImage.enabled = true;
            _lockImage.enabled = true;
            _unlockUnitText.enabled = true;

            _softCurrencyImage.enabled = false;
            _priceUpgradeUnitText.enabled = false;

            _isOpen = false;
        }

        public void OpenUnit()
        {
            _lockImage.enabled = false;
            _unlockUnitText.enabled = false;
            _softCurrencyImage.enabled = true;
            _priceUpgradeUnitText.enabled = true;
            _hardCurrencyImage.enabled = false;

            _isOpen = true;
        }

        public void DisablePrices()
        {
            _lockImage.enabled = false;
            _unlockUnitText.enabled = false;
            _softCurrencyImage.enabled = false;
            _priceUpgradeUnitText.enabled = false;
            _hardCurrencyImage.enabled = false;
        }

        public void SelectUnitUI()
        {
            SelectUnitAction?.Invoke(_defenceUnitType);
        }
    }
}
