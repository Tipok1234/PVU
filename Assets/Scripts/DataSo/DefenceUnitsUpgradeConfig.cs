using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Enums;

namespace Assets.Scripts.DataSo
{
    [CreateAssetMenu(fileName = "UpgradeDefence", menuName = "UpgradeSO/UpgradeDefence")]
    public class DefenceUnitsUpgradeConfig : ScriptableObject
    {
        [SerializeField] private DefenceUnitUpgradeData[] _defenceUnitUpgradeDatas;
        public DefenceUnitUpgradeDataModel DefenceUpgradeUnits(DefenceUnitType defenceUnitType, int level)
        {
            for (int i = 0; i < _defenceUnitUpgradeDatas.Length; i++)
            {
                if (_defenceUnitUpgradeDatas[i].DefenceUnitType == defenceUnitType)
                {
                    return _defenceUnitUpgradeDatas[i].DefenceUnitUpgradeDataModel[level];
                }
            }

            Debug.LogError("Config not found");
            return null;
        }

        public UnitCharacteristicData DefenceUpgradeUnit(DefenceUnitType defenceUnitType,int level, CharacteristicUnitType characteristicUnitType)
        {
            Debug.LogError(level);

            for (int i = 0; i < _defenceUnitUpgradeDatas.Length; i++)
            {
                if (_defenceUnitUpgradeDatas[i].DefenceUnitType == defenceUnitType)
                {

                    var unitDatas = _defenceUnitUpgradeDatas[i].DefenceUnitUpgradeDataModel[level].UnitCharacteristicDatas;

                    for (int j = 0; j < unitDatas.Length; j++)
                    {
                       // Debug.LogError(level);

                        if (unitDatas[j].CharacteristicUnitType == characteristicUnitType)
                        {
                            return unitDatas[j];
                        }
                    }
                }
            }

            Debug.LogError("Config not found");
            return null;
        }

        public bool IsMaxUnitLevel(DefenceUnitType defenceUnitType,int level)
        {
            for (int i = 0; i < _defenceUnitUpgradeDatas.Length; i++)
            {
                if (_defenceUnitUpgradeDatas[i].DefenceUnitType == defenceUnitType)
                {
                    return _defenceUnitUpgradeDatas[i].DefenceUnitUpgradeDataModel.Length - 1 == level;
                }
            }

            return false;
        }
    }
}
