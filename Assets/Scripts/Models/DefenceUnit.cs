using UnityEngine;
using Assets.Scripts.Enums;
using Assets.Scripts.DataSo;
using Assets.Scripts.Grids;
using System;

namespace Assets.Scripts.Models
{
    public class DefenceUnit : BaseUnit
    {
        public static event Action<DefenceUnitType> UnitSoldAction;
        public DefenceUnitType DefencUnitType => _unitDefenceType;

        [SerializeField] protected DefenceUnitType _unitDefenceType;
        [SerializeField] protected UnitDataSo _unitData;

        private void OnMouseDown()
        {
            if(Grids.Grid.IsSell)
            {
                UnitSoldAction?.Invoke(_unitDefenceType);
                Death();
            }
        }
    }
}
