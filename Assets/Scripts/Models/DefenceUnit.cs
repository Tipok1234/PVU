using UnityEngine;
using Assets.Scripts.Enums;
using Assets.Scripts.DataSo;

namespace Assets.Scripts.Models
{
    public class DefenceUnit : BaseUnit
    {
        public DefenceUnitType DefencUnitType => _unitDefenceType;

        [SerializeField] protected DefenceUnitType _unitDefenceType;
        [SerializeField] protected UnitDataSo _unitData;
    }
}
