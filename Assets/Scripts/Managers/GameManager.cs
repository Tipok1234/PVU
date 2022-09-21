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
        [SerializeField] private LevelManager _levelManager;

        [SerializeField] private LevelComplete _levelComplete;
        [SerializeField] private UnitDataSo[] _unitDataSo;

        private DataManager _dataManager;

        private int _softCurrency = 0;
        private int _currentGunpowder = 1000;

        private void Awake()
        {
            _grid.UnitSoldAction += OnUnitSold;
            _grid.CurrencyCollectedAction += OnCurrencyCollected;
            _grid.UnitCreateAction += OnUnitCreated;
            _gameUIController.UnitSelectedAction += OnUnitSelect;
            _gameOver.RestartGameAction += RestartGame;
            _enemyManager.LevelCompletedAction += OnLevelCompleted;

        }
        private void Start()
        {
            _dataManager = FindObjectOfType<DataManager>();
            //_dataManager.LoadData();
            _softCurrency = _dataManager.SoftCurrency;

            _grid.Setup(_width, _length);
            _enemyManager.Setup(_grid.EnemySpawnPoints, _levelManager.GetLevelByIndex(_dataManager.LevelIndex));
            _gameUIController.Setup(_unitDataSo);

            _gameUIController.UpdateSoftCurrency(_softCurrency);
            _gameUIController.UpdateGameCurrency(_currentGunpowder);
            _gameUIController.UpdateUnitGameUIItems(_currentGunpowder);
        }

        public void OnLevelCompleted()
        {
            _levelComplete.ShowWindow();
            _dataManager.UpdateLevel();
        }
        private void OnUnitSold(DefenceUnitType unitType)
        {
            int soldValue = 0;

            for (int i = 0; i < _unitDataSo.Length; i++)
            {
                if (_unitDataSo[i].DefencUnitType == unitType)
                {
                    soldValue = (int)(_unitDataSo[i].Price * 0.5f);
                    break;
                }
            }

            _currentGunpowder += soldValue;
            _gameUIController.UpdateGameCurrency(_currentGunpowder);
            _gameUIController.UpdateUnitGameUIItems(_currentGunpowder);
        }
        private void OnCurrencyCollected(int currencyAmount,CurrencyType currencyType)
        {
            switch(currencyType)
            {
                case CurrencyType.GameCurrency:
                    _currentGunpowder += currencyAmount;
                    _gameUIController.UpdateGameCurrency(_currentGunpowder);
                    _gameUIController.UpdateUnitGameUIItems(_currentGunpowder);
                    break;
                case CurrencyType.SoftCurrency:
                    _softCurrency += currencyAmount;
                    _gameUIController.UpdateSoftCurrency(_softCurrency);
                    _dataManager.AddCurrency(currencyAmount, currencyType);
                    break;

            }
        }

        public void OnUnitSelect(DefenceUnitType unitType)
        {

            for (int i = 0; i < _unitDataSo.Length; i++)
            {
                if (_unitDataSo[i].DefencUnitType == unitType)
                {
                    if (_currentGunpowder >= _unitDataSo[i].Price)
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
                    _currentGunpowder -= _unitDataSo[i].Price;
                    _gameUIController.UpdateGameCurrency(_currentGunpowder);
                    _gameUIController.UpdateUnitGameUIItems(_currentGunpowder);
                    _gameUIController.RechargePlaceCooldown(defenceUnit);
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
