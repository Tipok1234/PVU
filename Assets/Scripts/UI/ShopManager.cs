using UnityEngine;
using Assets.Scripts.Managers;
using Assets.Scripts.DataSo;
using Assets.Scripts.Enums;

namespace Assets.Scripts.UI
{
    public class ShopManager : MonoBehaviour
    {
        [SerializeField] private SelectHandManager _selectHandManager;
        [SerializeField] private ShopWindow _shopWindow;

        private DataManager _dataManager;

        private void Start()
        {
            _dataManager = FindObjectOfType<DataManager>();
            _dataManager.UpdateCurrencyAction += OnUpdateCurrency;

            _dataManager.LoadData();

            for (int i = 0; i < ConfigManager.Intsance.Config.UnitDataSos.Length; i++)
            {
                if (_dataManager.UnitsDictionary.TryGetValue(ConfigManager.Intsance.Config.UnitDataSos[i].DefencUnitType, out int level))
                {
                    ConfigManager.Intsance.Config.UnitDataSos[i].OpenUnit();
                    ConfigManager.Intsance.Config.UnitDataSos[i].SetLevel(level);
                    ConfigManager.Intsance.Config.UnitDataSos[i].SetCharacteristicData(ConfigManager.Intsance.Config.UpgradeConfig.DefenceUpgradeUnits(ConfigManager.Intsance.Config.UnitDataSos[i].DefencUnitType, level).UnitCharacteristicDatas);
                }
                else
                {
                    ConfigManager.Intsance.Config.UnitDataSos[i].ResetData();
                }
            }

            _shopWindow.BuyUnitAction += OnBuyUnit;

            _shopWindow.UpgradeUnitAction += OnUpgradeAction;
            _shopWindow.SelectUnitAction += OnSelectedAction;

            _selectHandManager.Setup(_dataManager);
        }

        private void OnUpdateCurrency(float amount, CurrencyType type)
        {
            _shopWindow.UpdateCurrency(amount,type);
        }

        private void OnBuyUnit(DefenceUnitType defenceUnitType)
        {

            for (int i = 0; i < ConfigManager.Intsance.Config.UpgradeConfig.DefenceUnitUpgradeDatas.Length; i++)
            {

                if (defenceUnitType != ConfigManager.Intsance.Config.UpgradeConfig.DefenceUnitUpgradeDatas[i].DefenceUnitType)
                    continue;

                DefenceUnitUpgradeData upgradeData = ConfigManager.Intsance.Config.UpgradeConfig.DefenceUnitUpgradeDatas[i];

                if (_dataManager.CheckCurrency(upgradeData.UnlockUnitPrice, upgradeData.CurrencyUnlockType))
                {
                    AudioManager.Instance.PlaySoundGame(AudioSoundType.LevelUpSound);

                    _dataManager.RemoveCurrency(upgradeData.UnlockUnitPrice, upgradeData.CurrencyUnlockType);

                    _dataManager.BuyUnit(defenceUnitType);

                    for (int j = 0; j < ConfigManager.Intsance.Config.UnitDataSos.Length; j++)
                    {
                        UnitDataSo unitDataSo = ConfigManager.Intsance.Config.UnitDataSos[j];

                        if (defenceUnitType == unitDataSo.DefencUnitType)
                        {
                            unitDataSo.OpenUnit();
                            unitDataSo.SetLevel(unitDataSo.Level);

                            unitDataSo.SetCharacteristicData(ConfigManager.Intsance.Config.UpgradeConfig.DefenceUpgradeUnits(unitDataSo.DefencUnitType, unitDataSo.Level).UnitCharacteristicDatas);

                            int upgradeCost = ConfigManager.Intsance.Config.UpgradeConfig.DefenceUpgradeUnits(unitDataSo.DefencUnitType, unitDataSo.Level).UpgradeCost;

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
            for (int i = 0; i < ConfigManager.Intsance.Config.UnitDataSos.Length; i++)
            {
                UnitDataSo unitDataSo = ConfigManager.Intsance.Config.UnitDataSos[i];

                if (defenceUnitType == unitDataSo.DefencUnitType)
                {

                    if (ConfigManager.Intsance.Config.UpgradeConfig.IsMaxUnitLevel(defenceUnitType, unitDataSo.Level))
                    {
                        _shopWindow.DisableUnitPrice();
                        return;
                    }

                    DefenceUnitUpgradeDataModel unitUpgrade = ConfigManager.Intsance.Config.UpgradeConfig.DefenceUpgradeUnits(defenceUnitType, unitDataSo.Level);

                    if (_dataManager.CheckCurrency(unitUpgrade.UpgradeCost, CurrencyType.SoftCurrency))
                    {
                        AudioManager.Instance.PlaySoundGame(AudioSoundType.LevelUpSound);
                        _dataManager.RemoveCurrency(unitUpgrade.UpgradeCost, CurrencyType.SoftCurrency);

                        _dataManager.LevelUpUnit(defenceUnitType);
                        unitDataSo.LevelUpUnit();

                        unitDataSo.SetCharacteristicData(ConfigManager.Intsance.Config.UpgradeConfig.DefenceUpgradeUnits(unitDataSo.DefencUnitType, unitDataSo.Level).UnitCharacteristicDatas);                      

                        int upgradeCost = ConfigManager.Intsance.Config.UpgradeConfig.DefenceUpgradeUnits(unitDataSo.DefencUnitType, unitDataSo.Level).UpgradeCost;
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
            for (int i = 0; i < ConfigManager.Intsance.Config.UnitDataSos.Length; i++)
            {
                if (ConfigManager.Intsance.Config.UnitDataSos[i].DefencUnitType == defenceUnitType)
                {
                    DefenceUnitUpgradeDataModel d1 = ConfigManager.Intsance.Config.UpgradeConfig.DefenceUpgradeUnits(defenceUnitType, ConfigManager.Intsance.Config.UnitDataSos[i].Level);

                    DefenceUnitUpgradeDataModel d2 = d1;

                    if (ConfigManager.Intsance.Config.UnitDataSos[i].IsOpen)
                    {
                        if (!ConfigManager.Intsance.Config.UpgradeConfig.IsMaxUnitLevel(defenceUnitType, ConfigManager.Intsance.Config.UnitDataSos[i].Level))
                        {
                            d2 = ConfigManager.Intsance.Config.UpgradeConfig.DefenceUpgradeUnits(defenceUnitType, ConfigManager.Intsance.Config.UnitDataSos[i].Level + 1);
                        }
                        else
                        {
                            _shopWindow.DisableUnitPrice();
                        }
                    }
                    _shopWindow.SelectUnit(d1, d2, ConfigManager.Intsance.Config.UnitDataSos[i].Level);
                    break;
                }
            }
        }

        [ContextMenu("ResetData")]
        public void ResetSO()
        {
            for (int i = 0; i < ConfigManager.Intsance.Config.UnitDataSos.Length; i++)
            {
                ConfigManager.Intsance.Config.UnitDataSos[i].ResetData();
            }
        }
    }
}
