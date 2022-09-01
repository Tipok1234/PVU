using UnityEngine;
using Assets.Scripts.Controller;
using Assets.Scripts.Enums;
using Assets.Scripts.Models;
using Assets.Scripts.UIManager;
using Assets.Scripts.DataSo;

namespace Assets.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        private int _width = 6;
        private int _length = 10;

        [SerializeField] private Grids.Grid _grid;
        [SerializeField] private EnemyManager _enemyManager;
        [SerializeField] private GameUIController _gameUIController;
        [SerializeField] private GameOver _gameOver;
        [SerializeField] private GameOverWindow _gameOverWindow;

        [SerializeField] private UnitDataSo[] _unitDataSo;

        private int _currentSoftCurrency = 1000;

        private void Awake()
        {
            _grid.UnitSoldAction += OnUnitSold;
            _grid.CurrencyCollectedAction += OnCurrencyCollected;
            _grid.UnitCreateAction += OnUnitCreated;
            _gameUIController.OnBuyUnitAction += BuyUnit;
            _gameOver.RestartGameAction += RestartGame;
        }
        private void Start()
        {
            _grid.Setup(_width, _length);
            _enemyManager.Setup(_grid.EnemySpawnPoints);
            _gameUIController.Setup(_unitDataSo);
        }

        private void OnUnitSold(int soldValue)
        {
            _currentSoftCurrency += soldValue;
            _gameUIController.UpdateSoftCurrency(_currentSoftCurrency);
        }
        private void OnCurrencyCollected(int softCurrency)
        {
            _currentSoftCurrency += softCurrency;
            _gameUIController.UpdateSoftCurrency(_currentSoftCurrency);
        }

        public void BuyUnit(DefenceUnitType unitType)
        {
            for (int i = 0; i < _unitDataSo.Length; i++)
            {
                if (_unitDataSo[i].DefencUnitType == unitType)
                {
                    if (_currentSoftCurrency >= _unitDataSo[i].Price)
                    {
                        _grid.StartPlaceUnit(unitType);
                        break;
                    }
                }
            }
        }
        public void OnUnitCreated(DefenceUnitType defenceUnit)
        {
            for (int i = 0; i < _unitDataSo.Length; i++)
            {
                if (_unitDataSo[i].DefencUnitType == defenceUnit)
                {
                    _currentSoftCurrency -= _unitDataSo[i].Price;
                    _gameUIController.UpdateSoftCurrency(_currentSoftCurrency);
                    break;
                }
            }
        }
        private void RestartGame()
        {
            _gameOverWindow.RestartGameUI();
        }
    }
}
