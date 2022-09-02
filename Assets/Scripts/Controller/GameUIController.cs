using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Assets.Scripts.DataSo;
using Assets.Scripts.UIManager;
using Assets.Scripts.Enums;
using UnityEngine.UI;
using System;

namespace Assets.Scripts.Controller
{
    public class GameUIController : MonoBehaviour
    {
        public event Action SellButtonAction;

        [SerializeField] private UnitGameUI _unitGameUIPrefab;
        [SerializeField] private Transform _spawnParent;

        [SerializeField] private Button _sellButton;

        [SerializeField] private Grids.Grid _grid;
        [SerializeField] private TMP_Text _scoreText;

        public event Action<DefenceUnitType> UnitSelectedAction;

        private List<UnitGameUI> _unitGameUIList = new List<UnitGameUI>();

        private void Awake()
        {
            _sellButton.onClick.AddListener(SellButton);
        }
        private void Start()
        {
            _scoreText.text = 100.ToString();
        }

        public void Setup(UnitDataSo[] unitDataSo)
        {
            ResetUI();

            for (int i = 0; i < unitDataSo.Length; i++)
            {
                UnitGameUI unitUI = Instantiate(_unitGameUIPrefab, _spawnParent);
                unitUI.Setup(unitDataSo[i]);
                unitUI.BuyUnitAction += OnBuyUnit;
                _unitGameUIList.Add(unitUI);
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
        public void SellButton()
        {
            SellButtonAction?.Invoke();
        }
        public void UpdateSoftCurrency(int softCurrencyAmount)
        {   
            _scoreText.text = softCurrencyAmount.ToString();
        }
    }
}
