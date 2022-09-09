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

        [SerializeField] private UnitDataSo[] _unitDataSO;

        private List<ShopUnitUIItem> _shopUnitUIItems = new List<ShopUnitUIItem>();

        private ShopUnitUIItem _selectedUnitUIItem;

        private void Awake()
        {
            _buyButton.onClick.AddListener(BuyUnitGame);
            
        }

        public void Start()
        {
            for (int i = 0; i < _unitDataSO.Length; i++)
            {
                ShopUnitUIItem shopUI = Instantiate(_shopUnitUIItemPrefab, _spawnUnitParent);
                shopUI.SelectUnitAction += OnUnitSelected;
                shopUI.Setup(_unitDataSO[i]);
                _shopUnitUIItems.Add(shopUI);
            }

            //Destroy(_imageUnit.gameObject);

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
                    for (int j = 0; j < _unitDataSO[i].UnitCharacteristicDatas.Length; j++)
                    {
                        UnitCharacteristicUIItem unitUI = Instantiate(_characteristicUnitUIPrefab, _spawnCharacteristicParent);
                        unitUI.Setup(_unitDataSO[i].UnitCharacteristicDatas[j]);
                        _imageUnit.sprite = _unitDataSO[i].UnitSprite;
                        _unitName.text = _unitDataSO[i].DefencUnitType.ToString();

                        _selectedUnitUIItem = _shopUnitUIItems[i];
                    }
                    break;
                }

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
            }
        }

        public void UpdateCurrency(int softCurrency)
        {
            _softCurrencyText.text = softCurrency.ToString();
        }
        private void BuyUnitGame()
        {
            if (_selectedUnitUIItem == null)
                return;

            _selectedUnitUIItem.OpenUnit();

            _buyButton.gameObject.SetActive(false);
            _upgradeButton.gameObject.SetActive(true);

            BuyButtonAction?.Invoke();
        }


    }
}
