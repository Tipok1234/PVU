using UnityEngine;
using Assets.Scripts.Controller;
using Assets.Scripts.Enums;
using Assets.Scripts.Models;
using Assets.Scripts.UIManager;
using Assets.Scripts.DataSo;
using Assets.Scripts.AnimationsModel;

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

        [SerializeField] private AnimationModel _animationModel;

        private DataManager _dataManager;

        private float _currentGunpowder = 1000;

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
            _dataManager.UpdateCurrencyAction += OnUpdatedCurrency;
            //_dataManager.LoadData();


            _grid.Setup(_width, _length);
            _enemyManager.Setup(_grid.EnemySpawnPoints, _levelManager.GetLevelByIndex(_dataManager.LevelIndex));

            _gameUIController.Setup(_unitDataSo, _dataManager);

            _gameUIController.UpdateCurrency(_currentGunpowder,CurrencyType.GameCurrency);
            _gameUIController.UpdateUnitGameUIItems(_currentGunpowder);


        }

        public void OnLevelCompleted()
        {
            _dataManager.UpdateLevel();
            _animationModel.PlayAnimation();
            _levelComplete.ShowWindow();
        }
        private void OnUnitSold(DefenceUnitType unitType)
        {
            int soldValue = 0;

            for (int i = 0; i < _unitDataSo.Length; i++)
            {
                if (_unitDataSo[i].DefencUnitType == unitType)
                {
                    soldValue = (int)(_unitDataSo[i].GetCharacteristicData(CharacteristicUnitType.Price) * 0.5f);
                    break;
                }
            }

            _currentGunpowder += soldValue;
            _gameUIController.UpdateCurrency(_currentGunpowder,CurrencyType.GameCurrency);
            _gameUIController.UpdateUnitGameUIItems(_currentGunpowder);
        }
        private void OnCurrencyCollected(float currencyAmount,CurrencyType currencyType)
        {
            switch(currencyType)
            {
                case CurrencyType.GameCurrency:
                    _currentGunpowder += currencyAmount;
                    _gameUIController.UpdateCurrency(_currentGunpowder,currencyType);
                    _gameUIController.UpdateUnitGameUIItems(_currentGunpowder);
                    break;
                case CurrencyType.SoftCurrency:
                    _dataManager.AddCurrency(currencyAmount, currencyType);
                    break;

            }
        }

        private void OnUpdatedCurrency(float currencyAmount, CurrencyType currencyType)
        {
            switch(currencyType)
            {
                case CurrencyType.HardCurrency:
                    _currentGunpowder += currencyAmount;
                    _gameUIController.UpdateCurrency(currencyAmount, currencyType);
                    break;
                case CurrencyType.SoftCurrency:
                    _gameUIController.UpdateCurrency(currencyAmount, currencyType);
                    break;
            }
        }

        public void OnUnitSelect(DefenceUnitType unitType)
        {
            for (int i = 0; i < _unitDataSo.Length; i++)
            {
                if (_unitDataSo[i].DefencUnitType == unitType)
                {
                    if (_currentGunpowder >= _unitDataSo[i].GetCharacteristicData(CharacteristicUnitType.Price))
                    {
                        var unit = PoolManager.Instance.GetDefenceUnitsByType(unitType, gameObject.transform);                    
                        _grid.StartPlaceUnit(unit);
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
                    _currentGunpowder -= _unitDataSo[i].GetCharacteristicData(CharacteristicUnitType.Price);
                    _gameUIController.UpdateCurrency(_currentGunpowder,CurrencyType.GameCurrency);
                    _gameUIController.UpdateUnitGameUIItems(_currentGunpowder);
                    _gameUIController.RechargePlaceCooldown(defenceUnit);
                    break;
                }
            }
        }
        private void RestartGame()
        {
            _animationModel.PlayAnimation();
            _gameOverWindow.RestartGameUI();           
        }
    }
}
