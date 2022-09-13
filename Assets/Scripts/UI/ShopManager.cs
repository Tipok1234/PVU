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

            _shopWindow.BuyUnitAction += OnBuyUnit;
            _shopWindow.UpgradeUnitAction += OnUpgradeAction;

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
                    _dataManager.RemoveCurrency(upgradeData.UnlockUnitPrice, upgradeData.CurrencyUnlockType);

                    _dataManager.BuyUnit(defenceUnitType);

                    for (int j = 0; j < _unitDataSO.Length; j++)
                    {
                        UnitDataSo unitDataSo = _unitDataSO[j];
                        
                        if(defenceUnitType == unitDataSo.DefencUnitType)
                        {
                            unitDataSo.OpenUnit();
                            unitDataSo.SetLevel(unitDataSo.Level);

                            _shopWindow.BuyUnit();
                            break;
                        }
                    }
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
                        return;

                    DefenceUnitUpgradeDataModel unitUpgrade = _defenceUnitsUpgradeConfig.DefenceUpgradeUnits(defenceUnitType, unitDataSo.Level);

                    if (_dataManager.CheckCurrency(unitUpgrade.UpgradeCost, CurrencyType.SoftCurrency))
                    {
                        _dataManager.RemoveCurrency(unitUpgrade.UpgradeCost, CurrencyType.SoftCurrency);

                        _dataManager.LevelUpUnit(defenceUnitType);
                        unitDataSo.LevelUpUnit();
                    }
                }
            }
        }
    }
}
