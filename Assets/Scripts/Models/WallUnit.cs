using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enums;
using UnityEngine;
using Assets.Scripts.Grids;

namespace Assets.Scripts.Models
{
    public class WallUnit : DefenceUnit
    {
        public override void TakeDamage(float damage)
        {
            _currentHP -= damage;

            if (_currentHP <= 0)
            {
                Death();
            }
        }
    }
}

