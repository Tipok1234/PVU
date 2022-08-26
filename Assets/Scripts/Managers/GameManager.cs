using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Controller;
using Assets.Scripts.Enums;
using Assets.Scripts.Models;
using Assets.Scripts.UIManager;

namespace Assets.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        private int _width = 6;
        private int _length = 10;

       [SerializeField] private Grids.Grid _grid;

       [SerializeField] private GameUIController _gameUIController;
        private int _currentSoftCurrency = 1000; 

        private void Awake()
        {
            _grid.CurrencyCollectedAction += OnCurrencyCollected;
            _gameUIController.OnBuyUnitAction += BuyUnit;
        }
        private void Start()
        {
            _grid.Setup(_width,_length);
            _gameUIController.InstantiateUnit();
        }

        public void OnCurrencyCollected(int softCurrency)
        {
            _currentSoftCurrency += softCurrency;
            _gameUIController.UpdateSoftCurrency(_currentSoftCurrency);
        }

        public void BuyUnit(UnitType unitType, int unitPrice)
        {
            if(_currentSoftCurrency >= unitPrice)
            {
                _currentSoftCurrency -= unitPrice;
                _grid.StartPlaceUnit(unitType);
                _gameUIController.UpdateSoftCurrency(_currentSoftCurrency);
            }               
        }
    }
}
