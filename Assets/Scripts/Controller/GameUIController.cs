using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Assets.Scripts.DataSo;
using Assets.Scripts.UIManager;
using Assets.Scripts.Managers;
using Assets.Scripts.Enums;
using UnityEngine.UI;
using System;

namespace Assets.Scripts.Controller
{
    public class GameUIController : MonoBehaviour
    {
        public TMP_Text SoftCurrency;

        public event Action SellButtonAction;

        [SerializeField] private UnitGameUI _unitGameUIPrefab;
        [SerializeField] private Transform _spawnParent;
        [SerializeField] private Canvas _optionCanvas;

        [SerializeField] private Button _sellButton;
        [SerializeField] private Button _optionButton;
        [SerializeField] private Button _exitOptionButton;

        [SerializeField] private Grids.Grid _grid;
        [SerializeField] private TMP_Text _gameCurrencyText;
        [SerializeField] private TMP_Text _softCurrencyText;

        public event Action<DefenceUnitType> UnitSelectedAction;

        private List<UnitGameUI> _unitGameUIList = new List<UnitGameUI>();

        private void Awake()
        {
            _sellButton.onClick.AddListener(SellButton);
            _optionButton.onClick.AddListener(OpenOptionCanvas);
            _exitOptionButton.onClick.AddListener(OpenOptionCanvas);
        }

        public void OpenOptionCanvas()
        {
            AudioManager.Instance.ClickSound();
            _optionCanvas.enabled = !_optionCanvas.enabled;
        }
        public void Setup(UnitDataSo[] unitDataSo) //, DataManager dataManager)
        {
            ResetUI();

            for (int i = 0; i < unitDataSo.Length; i++)
            {
              //  if(dataManager.UnitHandItems.Contains(unitDataSo[i].DefencUnitType))
              //  {
                    UnitGameUI unitUI = Instantiate(_unitGameUIPrefab, _spawnParent);
                    unitUI.Setup(unitDataSo[i]);
                    unitUI.BuyUnitAction += OnBuyUnit;
                    _unitGameUIList.Add(unitUI);
              //  }
            }
        }

        public void UpdateUnitGameUIItems(int gunPowder)
        {
            for (int i = 0; i < _unitGameUIList.Count; i++)
            {
                _unitGameUIList[i].HightLight(gunPowder >= _unitGameUIList[i].UnitPrice);
            }
        }
        private void ResetUI()
        {
            for (int i = 0; i < _unitGameUIList.Count; i++)
            {
                _unitGameUIList[i].BuyUnitAction -= OnBuyUnit;
            }

            _unitGameUIList.Clear();
        }
        private void OnBuyUnit(DefenceUnitType unitType)
        {

            UnitSelectedAction?.Invoke(unitType);
        }

        public void RechargePlaceCooldown(DefenceUnitType unitType)
        {
            for (int i = 0; i < _unitGameUIList.Count; i++)
            {
                if (_unitGameUIList[i].UnitType == unitType)
                {
                    _unitGameUIList[i].RechargeTime();
                    break;
                }
            }
        }
        public void SellButton()
        {
            SellButtonAction?.Invoke();
        }
        public void UpdateGameCurrency(int gameCurrencyAmount)
        {
            _gameCurrencyText.text = gameCurrencyAmount.ToString();
        }

        public void UpdateSoftCurrency(int softCurrencyAmount)
        {
            _softCurrencyText.text = softCurrencyAmount.ToString();
        }
    }
}
