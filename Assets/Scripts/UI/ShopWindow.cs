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
        public event Action BuyButtonAction;

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

        public void Start()
        {
            _dataManager = FindObjectOfType<DataManager>();

            for (int i = 0; i < _unitDataSO.Length; i++)
            {
                ShopUnitUIItem shopUI = Instantiate(_shopUnitUIItemPrefab, _spawnUnitParent);
                shopUI.SelectUnitAction += OnUnitSelected;
                shopUI.Setup(_unitDataSO[i]);
                _shopUnitUIItems.Add(shopUI);
            }

            _softCurrencyText.text = CurrencyType.SoftCurrency.ToString();
            _hardCurrencyText.text = CurrencyType.HardCurrency.ToString();
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

                        UnitCharacteristicData d2 = d1;

                        UnitCharacteristicUIItem unitUI = Instantiate(_characteristicUnitUIPrefab, _spawnCharacteristicParent);

                        if (_selectedUnitDataSO.IsOpen)
                        {
                            if(!_defenceUnitsUpgradeConfig.IsMaxUnitLevel(_selectedUnitDataSO.DefencUnitType, _selectedUnitDataSO.Level))
                            {
                                d2 = _defenceUnitsUpgradeConfig.DefenceUpgradeUnit(_selectedUnitDataSO.DefencUnitType,
                                _selectedUnitDataSO.Level + 1, _selectedUnitDataSO.UnitCharacteristicDatas[j].CharacteristicUnitType);
                            }
                        }

                        unitUI.Setup(d1, d2);

                        _currentLevelText.text = (_unitDataSO[i].Level+1).ToString();
                        _imageUnit.sprite = _unitDataSO[i].UnitSprite;
                        _unitName.text = _unitDataSO[i].DefencUnitType.ToString();
                    }


                    break;
                }


            }
        }

        //public void UpdateCurrency(int softCurrency)
        //{
        //    _softCurrencyText.text = softCurrency.ToString();
        //}

        private void BuyUnitGame()
        {
            if (_selectedUnitUIItem == null)
                return;

            _dataManager.BuyUnit(_selectedUnitDataSO.DefencUnitType);

            _selectedUnitUIItem.OpenUnit();
            _selectedUnitDataSO.OpenUnit();
            _selectedUnitDataSO.SetLevel(_selectedUnitDataSO.Level);

            _buyButton.gameObject.SetActive(false);
            _upgradeButton.gameObject.SetActive(true);

            BuyButtonAction?.Invoke();
        }

        private void UpgradeUnitButton()
        {
            if (_selectedUnitUIItem == null)
                return;

            if (_defenceUnitsUpgradeConfig.IsMaxUnitLevel(_selectedUnitDataSO.DefencUnitType, _selectedUnitDataSO.Level))
                return;

            _dataManager.LevelUpUnit(_selectedUnitDataSO.DefencUnitType);
            _selectedUnitDataSO.LevelUpUnit();
        }

    }
}
