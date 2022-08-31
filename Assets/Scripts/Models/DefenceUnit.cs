using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Models
{
    public class DefenceUnit : BaseUnit
    {
        public DefenceUnitType DefencUnitType => _unitDefenceType;

        [SerializeField] protected DefenceUnitType _unitDefenceType;
        public virtual int Sell()
        {
            base.Death();
            return 10;
        }
    }
}
