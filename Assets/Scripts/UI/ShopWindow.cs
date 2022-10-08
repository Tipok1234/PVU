using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.DataSo;
using Assets.Scripts.Enums;
using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace Assets.Scripts.UIManager
{
    public class ShopWindow : BaseWindow
    {
        public event Action<DefenceUnitType> BuyUnitAction;
        public event Action<DefenceUnitType> UpgradeUnitAction;
        public event Action<DefenceUnitType> SelectUnitAction;

        [SerializeField] private UnitCharacteristicUIItem _characteristicUnitUIPrefab;
        [SerializeField] private ShopUnitUIItem _shopUnitUIItemPrefab;
        [SerializeField] private Transform _spawnUnitImage;
        [SerializeField] private Transform _spawnUnitParent;
        [SerializeField] private Transform _spawnCharacteristicParent;

        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _upgradeButton;


        [SerializeField] private Image _imageUnit;
        [SerializeField] private TMP_Text _unitName;
        [SerializeField] private TMP_Text _softCurrencyText;
        [SerializeField] private TMP_Text _hardCurrencyText;
        [SerializeField] private TMP_Text _currentLevelText;

        [SerializeField] private DefenceUnitsUpgradeConfig _defenceUnitsUpgradeConfig;


        private List<ShopUnitUIItem> _shopUnitUIItems = new List<ShopUnitUIItem>();

        private ShopUnitUIItem _selectedUnitUIItem;

        private void Awake()
        {
            _buyButton.onClick.AddListener(BuyUnitGame);
            _upgradeButton.onClick.AddListener(UpgradeUnitButton);
            _closeWindowButton.onClick.AddListener(CloseWindow);
        }     

        public void Setup(UnitDataSo[] unitDataSo)
        {

            for (int i = 0; i < unitDataSo.Length; i++)
            {
                ShopUnitUIItem shopUI = Instantiate(_shopUnitUIItemPrefab, _spawnUnitParent);
                shopUI.SelectUnitAction += OnUnitSelected;


                shopUI.Setup(unitDataSo[i]);

                if (unitDataSo[i].IsOpen)
                {
                    if (_defenceUnitsUpgradeConfig.IsMaxUnitLevel(unitDataSo[i].DefencUnitType, unitDataSo[i].Level))
                    {
                        shopUI.DisablePrices();
                    }
                    else
                    {

                        var upgradeData = _defenceUnitsUpgradeConfig.DefenceUpgradeUnits(unitDataSo[i].DefencUnitType, unitDataSo[i].Level);
                        shopUI.UpdatePriceText(upgradeData.UpgradeCost, CurrencyType.SoftCurrency);
                    }
                }
                else
                {
                    int unlockPrice = _defenceUnitsUpgradeConfig.GetDefenceUnitUnlockPrice(unitDataSo[i].DefencUnitType);
                    shopUI.UpdatePriceText(unlockPrice, CurrencyType.HardCurrency);
                }

                _shopUnitUIItems.Add(shopUI);

            }

            OnUnitSelected(unitDataSo[0].DefencUnitType);
        }

        private void OnUnitSelected(DefenceUnitType defenceUnitType)
        {
            for (int i = _spawnCharacteristicParent.childCount - 1; i >= 0; i--)
            {
                Destroy(_spawnCharacteristicParent.GetChild(i).gameObject);
            }

            for (int i = 0; i < _shopUnitUIItems.Count; i++)
            {
                if (_shopUnitUIItems[i].DefenceUnitType == defenceUnitType)
                {
                    _selectedUnitUIItem = _shopUnitUIItems[i];
                    break;
                }
            }

            SelectUnitAction?.Invoke(_selectedUnitUIItem.DefenceUnitType);
        }

        public void DisableUnitPrice()
        {
            _selectedUnitUIItem.DisablePrices();
        }
        public void SelectUnit(DefenceUnitUpgradeDataModel d1, DefenceUnitUpgradeDataModel d2, int level)
        {
            if (_selectedUnitUIItem.IsOpen)
            {
                _buyButton.gameObject.SetActive(false);
                _upgradeButton.gameObject.SetActive(true);
            }
            else
            {
                _buyButton.gameObject.SetActive(true);
                _upgradeButton.gameObject.SetActive(false);
            }

            for (int i = 0; i < d1.UnitCharacteristicDatas.Length; i++)
            {
                UnitCharacteristicUIItem unitUI = Instantiate(_characteristicUnitUIPrefab, _spawnCharacteristicParent);

                unitUI.Setup(d1.UnitCharacteristicDatas[i], d2.UnitCharacteristicDatas[i]);
            }

            _currentLevelText.text = (level + 1).ToString();
            _imageUnit.sprite = _selectedUnitUIItem.UnitSprite;
            _unitName.text = _selectedUnitUIItem.DefenceUnitType.ToString();
        }

        public void UpdateCurrency(float amount, CurrencyType currencyType)
        {
            switch(currencyType)
            {
                case CurrencyType.SoftCurrency:
                    _softCurrencyText.text = amount.ToString();
                    break;

                case CurrencyType.HardCurrency:
                    _hardCurrencyText.text = amount.ToString();
                    break;
            }
        }

        private void BuyUnitGame()
        {
            if (_selectedUnitUIItem == null)
                return;

            BuyUnitAction?.Invoke(_selectedUnitUIItem.DefenceUnitType);
        }

        public void BuyUnit(int upgradeCost, CurrencyType currencyType)
        {
            _selectedUnitUIItem.OpenUnit();

            _buyButton.gameObject.SetActive(false);
            _upgradeButton.gameObject.SetActive(true);

            _selectedUnitUIItem.UpdatePriceText(upgradeCost, currencyType);

            OnUnitSelected(_selectedUnitUIItem.DefenceUnitType);
        }

        private void UpgradeUnitButton()
        {
            if (_selectedUnitUIItem == null)
                return;

            UpgradeUnitAction?.Invoke(_selectedUnitUIItem.DefenceUnitType);
        }
        public void UpgradeUnit(int upgradeCost, CurrencyType currencyType)
        {
            
            OnUnitSelected(_selectedUnitUIItem.DefenceUnitType);

            _selectedUnitUIItem.UpdatePriceText(upgradeCost, currencyType);
        }
    }
}