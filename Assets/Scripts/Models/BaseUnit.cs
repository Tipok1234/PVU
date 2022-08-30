using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Grids;

namespace Assets.Scripts.Models
{
    public class BaseUnit : MonoBehaviour
    {
        public float HP => _hp;
        public Collider ColliderUnit => _colliderUnit;

        [SerializeField] protected float _hp;
        [SerializeField] protected Collider _colliderUnit;


        protected bool _isDead = false;
        protected bool _isActive = false;

        public virtual void TakeDamage(float damage)
        {
            _hp -= damage;
        }

        public virtual void Create()
        {
            
        }

        public virtual void Death()
        {
            
        }
    }
}
