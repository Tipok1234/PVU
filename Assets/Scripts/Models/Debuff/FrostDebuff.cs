using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class FrostDebuff : BaseDebuff
    {
        public float SpeedDecreasePercent => _speedDecreasePercent;
        public FrostDebuff(float speedDecreasePercent, float debuffDuration)
        {
            _speedDecreasePercent = speedDecreasePercent;
            _debuffDuration = debuffDuration;
            _debuffType = Enums.DebuffType.Frost_Debuff;
        }

        [SerializeField] protected float _speedDecreasePercent;
    }
}
