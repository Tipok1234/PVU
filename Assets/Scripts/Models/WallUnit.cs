using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enums;
using UnityEngine;
using Assets.Scripts.Grids;

namespace Assets.Scripts.Models
{
    public class WallUnit : DefenceUnit
    {
        [SerializeField] private ParticleSystem _deathParticle;
        public override void TakeDamage(float damage)
        {
            _currentHP -= damage;

            if (_currentHP <= 0)
            {
                Death();
                _deathParticle.transform.position = gameObject.transform.position;
                var particleSystem = Instantiate(_deathParticle);
                Destroy(particleSystem, 2f);
            }
        }
    }
}

