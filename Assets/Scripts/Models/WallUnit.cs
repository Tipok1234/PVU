using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enums;
using UnityEngine;
using Assets.Scripts.Grids;

namespace Assets.Scripts.Models
{
    public class WallUnit : DefenceUnit
    {
        private void FixedUpdate()
        {

        }

        public override void TakeDamage(float damage)
        {
            _hp -= damage;

            if (_hp <= 0)
            {
                Death();
            }
        }
        public override void Death()
        {
            base.Death();
            _isDead = true;
            _colliderUnit.enabled = false;
            Destroy(gameObject);
        }
    }
}

