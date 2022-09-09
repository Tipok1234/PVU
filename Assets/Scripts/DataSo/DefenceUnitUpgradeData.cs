using UnityEngine;
using Assets.Scripts.Enums;

namespace Assets.Scripts.DataSo
{
    [System.Serializable]
    public class DefenceUnitUpgradeData
    {
        public DefenceUnitType DefenceUnitType => _defenceUnitType;
        public DefenceUnitUpgradeDataModel[] DefenceUnitUpgradeDataModel => _defenceUnitUpgradeDataModel;

        [SerializeField] private DefenceUnitType _defenceUnitType;

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
