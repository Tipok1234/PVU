using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.DataSo;

namespace Assets.Scripts.Config
{
    public class Config : MonoBehaviour
    {
        public UnitDataSo[] UnitDataSos => _unitDataSOs;

        [SerializeField] private UnitDataSo[] _unitDataSOs;
        [SerializeField] private DefenceUnitsUpgradeConfig _defenceUnitsUpgradeConfig;

        public interface IConfig
        {

        }
    }
}
