using UnityEngine;
using Assets.Scripts.Controller;
using Assets.Scripts.Enums;
using Assets.Scripts.Models;
using Assets.Scripts.UI;
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

        private float _currentGunpowder = 1000;

        private void Awake()
        {
            _grid.UnitSoldAction += OnUnitSold;
            _grid.CurrencyCollectedAction += OnCurrencyCollected;
            _grid.UnitCreateAction += OnUnitCreated;
            _gameUIController.UnitSelectedAction += OnUnitSelect;
            _gameOver.RestartGameAction += RestartGame;
            _enemyManager.LevelCompletedAction += OnLevelCompleted;
            _gameUIController.SkillSelectAction += OnSkillSelect;
        }
        private void Start()
        {
            _dataManager = FindObjectOfType<DataManager>();
            _dataManager.UpdateCurrencyAction += OnUpdatedCurrency;

            _grid.Setup(_width, _length);
            _enemyManager.Setup(_grid.EnemySpawnPoints, _levelManager.GetLevelByIndex(_dataManager.LevelIndex));
            _levelManager.UpdateLevel(_dataManager.LevelIndex);

            _gameUIController.Setup(_unitDataSo, _dataManager);

            _gameUIController.UpdateCurrency(_currentGunpowder,CurrencyType.GameCurrency);
            OnCurrencyCollected(0f, CurrencyType.SoftCurrency);
            OnCurrencyCollected(0f, CurrencyType.HardCurrency);
            _gameUIController.UpdateUnitGameUIItems(_currentGunpowder);
            _gameUIController.UpdateHightLightSkillUI(_dataManager.HardCurrency);

        }

        public void OnLevelCompleted()
        {
            _dataManager.UpdateLevel();
            _levelComplete.ShowWindow();
            OnCurrencyCollected(100f,CurrencyType.SoftCurrency);
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
                case CurrencyType.HardCurrency:
                    _dataManager.AddCurrency(currencyAmount, currencyType);
                    break;

            }
        }

        private void OnUpdatedCurrency(float currencyAmount, CurrencyType currencyType)
        {
            switch(currencyType)
            {
                case CurrencyType.HardCurrency:
                    _gameUIController.UpdateCurrency(currencyAmount, currencyType);
                    break;
                case CurrencyType.SoftCurrency:
                    _gameUIController.UpdateCurrency(currencyAmount, currencyType);
                    break;
            }
        }

        private void OnUnitSelect(DefenceUnitType unitType)
        {

            for (int i = 0; i < _unitDataSo.Length; i++)
            {
                if (_unitDataSo[i].DefencUnitType == unitType)
                {
                    if (_currentGunpowder >= _unitDataSo[i].GetCharacteristicData(CharacteristicUnitType.Price))
                    {
                        var unit = PoolManager.Instance.GetDefenceUnitsByType(unitType);
                        _grid.StartPlaceUnit(unit);
                        break;
                    }
                }
            }
        }
        private void OnUnitCreated(DefenceUnitType defenceUnit)
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

        private void OnSkillSelect(SkillType skillType,float price)
        {
            if (_dataManager.HardCurrency >= price)
            {
                _gameUIController.UpdateHightLightSkillUI(_dataManager.HardCurrency);

                switch (skillType)
                {
                    case SkillType.Rain:
                        _gameUIController.UpdateCurrency(price, CurrencyType.HardCurrency);
                        _dataManager.RemoveCurrency((int)price, CurrencyType.HardCurrency);
                        _gameUIController.UseSkill(skillType,price);
                        break;
                    case SkillType.Frost:
                        _gameUIController.UpdateCurrency(price, CurrencyType.HardCurrency);
                        _dataManager.RemoveCurrency((int)price, CurrencyType.HardCurrency);
                        _gameUIController.UseSkill(skillType, price);
                        break;
                }
            }
            else
            {
                _gameUIController.UpdateHightLightSkillUI(_dataManager.HardCurrency);
            }
        }
        private void RestartGame()
        {
            _gameOverWindow.RestartGameUI();           
        }
    }
}
