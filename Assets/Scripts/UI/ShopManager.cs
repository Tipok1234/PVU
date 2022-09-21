using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Managers;
using Assets.Scripts.DataSo;
using Assets.Scripts.Enums;
using System;

namespace Assets.Scripts.UIManager
{
    public class ShopManager : MonoBehaviour
    {
        [SerializeField] private ShopWindow _shopWindow;
        [SerializeField] private UnitDataSo[] _unitDataSO;
        [SerializeField] private DefenceUnitsUpgradeConfig _defenceUnitsUpgradeConfig;
        [SerializeField] private ShopUnitUIItem _shopUnitUIItemPrefab;

        private DataManager _dataManager;

        private void Start()
        {
            _dataManager = FindObjectOfType<DataManager>();

            _dataManager.LoadData();

            for (int i = 0; i < _unitDataSO.Length; i++)
            {
                if (_dataManager.UnitsDictionary.TryGetValue(_unitDataSO[i].DefencUnitType, out int level))
                {
                    Debug.LogError("OpenUnit " + _unitDataSO[i].DefencUnitType);
                    _unitDataSO[i].OpenUnit();
                    _unitDataSO[i].SetLevel(level);
                }
                else
                {
                    _unitDataSO[i].ResetData();
                }
            }

            _shopWindow.BuyUnitAction += OnBuyUnit;
            _shopWindow.UpgradeUnitAction += OnUpgradeAction;
            _shopWindow.SelectUnitAction += OnSelectedAction;

            _shopWindow.Setup(_unitDataSO);
        }

        private void OnBuyUnit(DefenceUnitType defenceUnitType)
        {
            for (int i = 0; i < _defenceUnitsUpgradeConfig.DefenceUnitUpgradeDatas.Length; i++)
            {

                if (defenceUnitType != _defenceUnitsUpgradeConfig.DefenceUnitUpgradeDatas[i].DefenceUnitType)
                    continue;

                DefenceUnitUpgradeData upgradeData = _defenceUnitsUpgradeConfig.DefenceUnitUpgradeDatas[i];

                if (_dataManager.CheckCurrency(upgradeData.UnlockUnitPrice, upgradeData.CurrencyUnlockType))
                {
                    AudioManager.Instance.LevelUpSound();

                    _dataManager.RemoveCurrency(upgradeData.UnlockUnitPrice, upgradeData.CurrencyUnlockType);

                    _dataManager.BuyUnit(defenceUnitType);

                    for (int j = 0; j < _unitDataSO.Length; j++)
                    {
                        UnitDataSo unitDataSo = _unitDataSO[j];

                        if (defenceUnitType == unitDataSo.DefencUnitType)
                        {
                            unitDataSo.OpenUnit();
                            unitDataSo.SetLevel(unitDataSo.Level);


                            int upgradeCost = _defenceUnitsUpgradeConfig.DefenceUpgradeUnits(unitDataSo.DefencUnitType, unitDataSo.Level).UpgradeCost;

                            _shopWindow.BuyUnit(upgradeCost, CurrencyType.SoftCurrency);
                            break;
                        }
                    }
                }
                else
                {
                    AudioManager.Instance.NoMoneySound();
                }
            }
        }

        private void OnUpgradeAction(DefenceUnitType defenceUnitType)
        {
            for (int i = 0; i < _unitDataSO.Length; i++)
            {
                UnitDataSo unitDataSo = _unitDataSO[i];

                if (defenceUnitType == unitDataSo.DefencUnitType)
                {

                    if (_defenceUnitsUpgradeConfig.IsMaxUnitLevel(defenceUnitType, unitDataSo.Level))
                    {
                        _shopWindow.DisableUnitPrice();
                        return;
                    }

                    Debug.LogError(unitDataSo.Level);

                    DefenceUnitUpgradeDataModel unitUpgrade = _defenceUnitsUpgradeConfig.DefenceUpgradeUnits(defenceUnitType, unitDataSo.Level);

                    if (_dataManager.CheckCurrency(unitUpgrade.UpgradeCost, CurrencyType.SoftCurrency))
                    {
                        AudioManager.Instance.LevelUpSound();
                        _dataManager.RemoveCurrency(unitUpgrade.UpgradeCost, CurrencyType.SoftCurrency);

                        Debug.LogError(unitUpgrade.UpgradeCost);

                        _dataManager.LevelUpUnit(defenceUnitType);
                        unitDataSo.LevelUpUnit();

                        int upgradeCost = _defenceUnitsUpgradeConfig.DefenceUpgradeUnits(unitDataSo.DefencUnitType, unitDataSo.Level).UpgradeCost;
                        _shopWindow.UpgradeUnit(upgradeCost, CurrencyType.SoftCurrency);

                    }
                    else
                    {
                        AudioManager.Instance.NoMoneySound();
                    }
                }
            }
        }

        private void OnSelectedAction(DefenceUnitType defenceUnitType)
        {
            for (int i = 0; i < _unitDataSO.Length; i++)
            {
                if (_unitDataSO[i].DefencUnitType == defenceUnitType)
                {
                    DefenceUnitUpgradeDataModel d1 = _defenceUnitsUpgradeConfig.DefenceUpgradeUnits(defenceUnitType, _unitDataSO[i].Level);

                    DefenceUnitUpgradeDataModel d2 = d1;

                    if (_unitDataSO[i].IsOpen)
                    {
                        if (!_defenceUnitsUpgradeConfig.IsMaxUnitLevel(defenceUnitType, _unitDataSO[i].Level))
                        {
                            d2 = _defenceUnitsUpgradeConfig.DefenceUpgradeUnits(defenceUnitType, _unitDataSO[i].Level+1);
                        }
                    }
                    _shopWindow.SelectUnit(d1, d2, _unitDataSO[i].Level);
                    break;
                }
            }
        }

        [ContextMenu("ResetData")]
        public void ResetSO()
        {
            for (int i = 0; i < _unitDataSO.Length; i++)
            {
                _unitDataSO[i].ResetData();
            }
        }
    }
}
