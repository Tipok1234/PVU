using UnityEngine;
using System;

namespace Assets.Scripts.Models
{
    public class BaseUnit : MonoBehaviour
    {
        public event Action UnitDeadAction;
        public float HP => _hp;
        public bool IsDead => _isDead;
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
            _isActive = true;
            _colliderUnit.enabled = true;
        }

        public virtual void Death(float deathTime = 0)
        {
            UnitDeadAction?.Invoke();
            _isDead = true;
            _isActive = false;
            _colliderUnit.enabled = false;
            Destroy(gameObject,deathTime);
        }
    }
}
