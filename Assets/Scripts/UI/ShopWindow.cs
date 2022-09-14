using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.DataSo;
using Assets.Scripts.Enums;
using Assets.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace Assets.Scripts.UIManager
{
    public class ShopWindow : MonoBehaviour
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

        [SerializeField] private UnitDataSo[] _unitDataSO;
        [SerializeField] private DefenceUnitsUpgradeConfig _defenceUnitsUpgradeConfig;
        private DataManager _dataManager;

        private List<ShopUnitUIItem> _shopUnitUIItems = new List<ShopUnitUIItem>();

        private ShopUnitUIItem _selectedUnitUIItem;

        private UnitDataSo _selectedUnitDataSO;

        private void Awake()
        {
            _buyButton.onClick.AddListener(BuyUnitGame);
            _upgradeButton.onClick.AddListener(UpgradeUnitButton);         
        }
        public void Setup(UnitDataSo[] unitDataSo)
        {
            _dataManager = FindObjectOfType<DataManager>();

            for (int i = 0; i < unitDataSo.Length; i++)
            {
                ShopUnitUIItem shopUI = Instantiate(_shopUnitUIItemPrefab, _spawnUnitParent);
                shopUI.SelectUnitAction += OnUnitSelected;

                if (unitDataSo[i].IsOpen)
                {
                    var upgradeData = _defenceUnitsUpgradeConfig.DefenceUpgradeUnits(_unitDataSO[i].DefencUnitType, _unitDataSO[i].Level);
                    shopUI.UpdatePriceText(upgradeData.UpgradeCost,CurrencyType.SoftCurrency);
                }
                else
                {
                    int unlockPrice =  _defenceUnitsUpgradeConfig.GetDefenceUnitUnlockPrice(_unitDataSO[i].DefencUnitType);
                    shopUI.UpdatePriceText(unlockPrice,CurrencyType.HardCurrency);
                }

                shopUI.Setup(unitDataSo[i]);
                _shopUnitUIItems.Add(shopUI);
            }


            UpdateCurrency();

            OnUnitSelected(unitDataSo[0].DefencUnitType);
        }

        private void OnUnitSelected(DefenceUnitType defenceUnitType)
        {
            for (int i = _spawnCharacteristicParent.childCount - 1; i >= 0; i--)
            {
                Destroy(_spawnCharacteristicParent.GetChild(i).gameObject);
            }


            for (int i = 0; i < _unitDataSO.Length; i++)
            {
                if (_unitDataSO[i].DefencUnitType == defenceUnitType)
                {

                    if (_unitDataSO[i].IsOpen)
                    {
                        _buyButton.gameObject.SetActive(false);
                        _upgradeButton.gameObject.SetActive(true);
                    }
                    else
                    {
                        _buyButton.gameObject.SetActive(true);
                        _upgradeButton.gameObject.SetActive(false);
                    }

                    for (int j = 0; j < _unitDataSO[i].UnitCharacteristicDatas.Length; j++)
                    {
                        _selectedUnitUIItem = _shopUnitUIItems[i];
                        _selectedUnitDataSO = _unitDataSO[i];

                        UnitCharacteristicData d1 = _defenceUnitsUpgradeConfig.DefenceUpgradeUnit(_selectedUnitDataSO.DefencUnitType,
                        _selectedUnitDataSO.Level, _selectedUnitDataSO.UnitCharacteristicDatas[j].CharacteristicUnitType);

                        Debug.LogError(_selectedUnitDataSO.Level);


                        UnitCharacteristicData d2 = d1;

                        UnitCharacteristicUIItem unitUI = Instantiate(_characteristicUnitUIPrefab, _spawnCharacteristicParent);


                        if (_selectedUnitDataSO.IsOpen)
                        {
                            if (!_defenceUnitsUpgradeConfig.IsMaxUnitLevel(_selectedUnitDataSO.DefencUnitType, _selectedUnitDataSO.Level))
                            {
                                d2 = _defenceUnitsUpgradeConfig.DefenceUpgradeUnit(_selectedUnitDataSO.DefencUnitType,
                                _selectedUnitDataSO.Level + 1, _selectedUnitDataSO.UnitCharacteristicDatas[j].CharacteristicUnitType);
                            }
                        }

                        unitUI.Setup(d1, d2);

                        _currentLevelText.text = (_selectedUnitDataSO.Level + 1).ToString();
                        _imageUnit.sprite = _selectedUnitDataSO.UnitSprite;
                        _unitName.text = _selectedUnitDataSO.DefencUnitType.ToString();

                        SelectUnitAction?.Invoke(_selectedUnitDataSO.DefencUnitType);
                    }

                    break;
                }
            }
        }

        public void UpdateCurrency()
        {
            _softCurrencyText.text = _dataManager.SoftCurrency.ToString();
            _hardCurrencyText.text = _dataManager.HardCurrency.ToString();
        }

        private void BuyUnitGame()
        {
            if (_selectedUnitUIItem == null)
                return;

            BuyUnitAction?.Invoke(_selectedUnitDataSO.DefencUnitType);
        }

        public void BuyUnit()
        {
            _hardCurrencyText.text = _dataManager.HardCurrency.ToString();

            _selectedUnitUIItem.OpenUnit();

            _buyButton.gameObject.SetActive(false);
            _upgradeButton.gameObject.SetActive(true);


            DefenceUnitUpgradeDataModel unitUpgrade = _defenceUnitsUpgradeConfig.DefenceUpgradeUnits(_selectedUnitDataSO.DefencUnitType, _selectedUnitDataSO.Level);
            _selectedUnitUIItem.UpdatePriceText(unitUpgrade.UpgradeCost,CurrencyType.SoftCurrency);

            OnUnitSelected(_selectedUnitDataSO.DefencUnitType);
        }

        private void UpgradeUnitButton()
        {
            if (_selectedUnitUIItem == null)
                return;

            UpgradeUnitAction?.Invoke(_selectedUnitDataSO.DefencUnitType);
        }
        public void UpgradeUnit()
        {
            _softCurrencyText.text = _dataManager.SoftCurrency.ToString();

            OnUnitSelected(_selectedUnitDataSO.DefencUnitType);

            DefenceUnitUpgradeDataModel unitUpgrade = _defenceUnitsUpgradeConfig.DefenceUpgradeUnits(_selectedUnitDataSO.DefencUnitType, _selectedUnitDataSO.Level);

            _selectedUnitUIItem.UpdatePriceText(unitUpgrade.UpgradeCost,CurrencyType.SoftCurrency);
        }
    }
}