using UnityEngine;
using Assets.Scripts.Enums;
using Assets.Scripts.UI;

namespace Assets.Scripts.DataSo
{
    [System.Serializable]
    public class DefenceUnitUpgradeData
    {
        public DefenceUnitType DefenceUnitType => _defenceUnitType;
        public CurrencyType CurrencyUnlockType => _currencyUnlockType;
        public int UnlockUnitPrice => _unlockUnitPrice;
        public DefenceUnitUpgradeDataModel[] DefenceUnitUpgradeDataModel => _defenceUnitUpgradeDataModel;

        [SerializeField] private DefenceUnitType _defenceUnitType;
        [SerializeField] private CurrencyType _currencyUnlockType;
        [SerializeField] private int _unlockUnitPrice;

        [SerializeField] private DefenceUnitUpgradeDataModel[] _defenceUnitUpgradeDataModel;
    }

    [System.Serializable]
    public class DefenceUnitUpgradeDataModel
    {
        public int UpgradeCost => _upgradeCost;
        public UnitCharacteristicData[] UnitCharacteristicDatas => _unitCharacteristicDatas;

        [SerializeField] private UnitCharacteristicData[] _unitCharacteristicDatas;
        [SerializeField] private int _upgradeCost;
    }
}
