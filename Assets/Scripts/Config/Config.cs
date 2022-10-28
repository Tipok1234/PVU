using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.DataSo;

namespace Assets.Scripts.Config
{
    [CreateAssetMenu(fileName = "MainConfig", menuName = "MainConfig/Config")]
    public class Config : ScriptableObject
    {
        public UnitDataSo[] UnitDataSos => _unitDataSOs;
        public DefenceUnitsUpgradeConfig UpgradeConfig => _defenceUnitsUpgradeConfig;

        [SerializeField] private UnitDataSo[] _unitDataSOs;
        [SerializeField] private DefenceUnitsUpgradeConfig _defenceUnitsUpgradeConfig;

    }
}
