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
            for (int i = 0; i < _defenceUnitUpgradeDatas.Length; i++)
            {
                if (_defenceUnitUpgradeDatas[i].DefenceUnitType == defenceUnitType)
                {
                    var unitDatas = _defenceUnitUpgradeDatas[i].DefenceUnitUpgradeDataModel[level].UnitCharacteristicDatas;

                    for (int j = 0; j < unitDatas.Length; j++)
                    {
                        if(unitDatas[j].CharacteristicUnitType == characteristicUnitType)
                        {
                            return unitDatas[j];
                        }
                    }

                }






                //for (int j = 0; j < _defenceUnitUpgradeDatas[i].DefenceUnitUpgradeDataModel.Length; i++)
                //{
                //    for (int k = 0; k < _defenceUnitUpgradeDatas[i].DefenceUnitUpgradeDataModel[j].UnitCharacteristicDatas.Length; k++)
                //    {
                //        if (_defenceUnitUpgradeDatas[i].DefenceUnitType == defenceUnitType && _defenceUnitUpgradeDatas[i].DefenceUnitUpgradeDataModel[j].
                //            UnitCharacteristicDatas[k].CharacteristicUnitType == characteristicUnitType)
                //        {
                //            return _defenceUnitUpgradeDatas[i].DefenceUnitUpgradeDataModel[level].UnitCharacteristicDatas[k];
                //        }
                //    }

                //}
            }

            Debug.LogError("Config not found");
            return null;
        }
    }
}
