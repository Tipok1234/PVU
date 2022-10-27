using UnityEngine;
using Assets.Scripts.Managers;
using Assets.Scripts.DataSo;
using Assets.Scripts.Enums;
using Assets.Scripts.Config;

namespace Assets.Scripts.UI
{
    public class ShopManager : MonoBehaviour
    {
        [SerializeField] private SelectHandManager _selectHandManager;
        [SerializeField] private ShopWindow _shopWindow;
        [SerializeField] private Config.Config _config;

        //[SerializeField] private UnitDataSo[] _unitDataSO;
        //[SerializeField] private DefenceUnitsUpgradeConfig _defenceUnitsUpgradeConfig;

        private DataManager _dataManager;

        private void Start()
        {
            _dataManager = FindObjectOfType<DataManager>();
            _dataManager.UpdateCurrencyAction += OnUpdateCurrency;

            _dataManager.LoadData();

            for (int i = 0; i < _config.UnitDataSos.Length; i++)
            {
                if (_dataManager.UnitsDictionary.TryGetValue(_config.UnitDataSos[i].DefencUnitType, out int level))
                {
                    _config.UnitDataSos[i].OpenUnit();
                    _config.UnitDataSos[i].SetLevel(level);
                    _config.UnitDataSos[i].SetCharacteristicData(_config.UpgradeConfig.DefenceUpgradeUnits(_config.UnitDataSos[i].DefencUnitType, level).UnitCharacteristicDatas);
                }
                else
                {
                    _config.UnitDataSos[i].ResetData();
                }
            }

            _shopWindow.BuyUnitAction += OnBuyUnit;

            _shopWindow.UpgradeUnitAction += OnUpgradeAction;
            _shopWindow.SelectUnitAction += OnSelectedAction;

            //_shopWindow.Setup(_unitDataSO);
            _selectHandManager.Setup(_dataManager);
        }

        private void OnUpdateCurrency(float amount, CurrencyType type)
        {
            _shopWindow.UpdateCurrency(amount,type);
        }

        private void OnBuyUnit(DefenceUnitType defenceUnitType)
        {
            for (int i = 0; i < _config.UpgradeConfig.DefenceUnitUpgradeDatas.Length; i++)
            {

                if (defenceUnitType != _config.UpgradeConfig.DefenceUnitUpgradeDatas[i].DefenceUnitType)
                    continue;

                DefenceUnitUpgradeData upgradeData = _config.UpgradeConfig.DefenceUnitUpgradeDatas[i];

                if (_dataManager.CheckCurrency(upgradeData.UnlockUnitPrice, upgradeData.CurrencyUnlockType))
                {
                    AudioManager.Instance.PlaySoundGame(AudioSoundType.LevelUpSound);

                    _dataManager.RemoveCurrency(upgradeData.UnlockUnitPrice, upgradeData.CurrencyUnlockType);

                    _dataManager.BuyUnit(defenceUnitType);

                    for (int j = 0; j < _config.UnitDataSos.Length; j++)
                    {
                        UnitDataSo unitDataSo = _config.UnitDataSos[j];

                        if (defenceUnitType == unitDataSo.DefencUnitType)
                        {
                            unitDataSo.OpenUnit();
                            unitDataSo.SetLevel(unitDataSo.Level);

                            unitDataSo.SetCharacteristicData(_config.UpgradeConfig.DefenceUpgradeUnits(unitDataSo.DefencUnitType, unitDataSo.Level).UnitCharacteristicDatas);

                            int upgradeCost = _config.UpgradeConfig.DefenceUpgradeUnits(unitDataSo.DefencUnitType, unitDataSo.Level).UpgradeCost;

                            _shopWindow.BuyUnit(upgradeCost, CurrencyType.SoftCurrency);

                            break;
                        }
                    }
                }
                else
                {
                    AudioManager.Instance.PlaySoundGame(AudioSoundType.NoMoneySound);
                }
            }
        }

        private void OnUpgradeAction(DefenceUnitType defenceUnitType)
        {
            for (int i = 0; i < _config.UnitDataSos.Length; i++)
            {
                UnitDataSo unitDataSo = _config.UnitDataSos[i];

                if (defenceUnitType == unitDataSo.DefencUnitType)
                {

                    if (_config.UpgradeConfig.IsMaxUnitLevel(defenceUnitType, unitDataSo.Level))
                    {
                        _shopWindow.DisableUnitPrice();
                        return;
                    }

                    DefenceUnitUpgradeDataModel unitUpgrade = _config.UpgradeConfig.DefenceUpgradeUnits(defenceUnitType, unitDataSo.Level);

                    if (_dataManager.CheckCurrency(unitUpgrade.UpgradeCost, CurrencyType.SoftCurrency))
                    {
                        AudioManager.Instance.PlaySoundGame(AudioSoundType.LevelUpSound);
                        _dataManager.RemoveCurrency(unitUpgrade.UpgradeCost, CurrencyType.SoftCurrency);

                        _dataManager.LevelUpUnit(defenceUnitType);
                        unitDataSo.LevelUpUnit();

                        unitDataSo.SetCharacteristicData(_config.UpgradeConfig.DefenceUpgradeUnits(unitDataSo.DefencUnitType, unitDataSo.Level).UnitCharacteristicDatas);                      

                        int upgradeCost = _config.UpgradeConfig.DefenceUpgradeUnits(unitDataSo.DefencUnitType, unitDataSo.Level).UpgradeCost;
                        _shopWindow.UpgradeUnit(upgradeCost, CurrencyType.SoftCurrency);

                    }
                    else
                    {
                        AudioManager.Instance.PlaySoundGame(AudioSoundType.NoMoneySound);
                    }
                }
            }
        }

        private void OnSelectedAction(DefenceUnitType defenceUnitType)
        {
            for (int i = 0; i < _config.UnitDataSos.Length; i++)
            {
                if (_config.UnitDataSos[i].DefencUnitType == defenceUnitType)
                {
                    DefenceUnitUpgradeDataModel d1 = _config.UpgradeConfig.DefenceUpgradeUnits(defenceUnitType, _config.UnitDataSos[i].Level);

                    DefenceUnitUpgradeDataModel d2 = d1;

                    if (_config.UnitDataSos[i].IsOpen)
                    {
                        if (!_config.UpgradeConfig.IsMaxUnitLevel(defenceUnitType, _config.UnitDataSos[i].Level))
                        {
                            d2 = _config.UpgradeConfig.DefenceUpgradeUnits(defenceUnitType, _config.UnitDataSos[i].Level + 1);
                        }
                        else
                        {
                            _shopWindow.DisableUnitPrice();
                        }
                    }
                    _shopWindow.SelectUnit(d1, d2, _config.UnitDataSos[i].Level);
                    break;
                }
            }
        }

        [ContextMenu("ResetData")]
        public void ResetSO()
        {
            for (int i = 0; i < _config.UnitDataSos.Length; i++)
            {
                _config.UnitDataSos[i].ResetData();
            }
        }
    }
}
