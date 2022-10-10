using Assets.Scripts.Enums;
using UnityEngine;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Models
{
    public class WallUnit : DefenceUnit
    {
        [SerializeField] private ParticleSystem _deathParticle;

        public override void Create()
        {
            base.Create();

            _currentHP = _unitData.GetCharacteristicData(CharacteristicUnitType.HP);
        }
        public override void TakeDamage(float damage)
        {
            _currentHP -= damage;

            if (_currentHP <= 0)
            {
                Death();
                PoolManager.Instance.GetParticleByType(ParticleType.Death_Type, gameObject.transform);
            }
        }
    }
}

