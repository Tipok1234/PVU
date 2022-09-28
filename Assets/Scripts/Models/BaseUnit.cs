using UnityEngine;
using System;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Models
{
    public class BaseUnit : MonoBehaviour
    {
        public event Action UnitDeadAction;
        public float HP => _hp;
        public float CurrentHP => _currentHP;
        public bool IsDead => _isDead;
        public Collider ColliderUnit => _colliderUnit;

        [SerializeField] protected float _hp;
        [SerializeField] protected Collider _colliderUnit;

        protected float _currentHP;

        protected bool _isDead = false;
        protected bool _isActive = false;

        public virtual void TakeDamage(float damage)
        {
            _currentHP -= damage;
        }

        public virtual void Create()
        {
            _isActive = true;
            _colliderUnit.enabled = true;
            _currentHP = _hp;
        }

        public virtual void Refresh()
        {
            _currentHP = _hp;
        }

        public virtual void Death(float deathTime = 0)
        {
           // UnitDeadAction?.Invoke();
            _isDead = true;
            _isActive = false;
            _colliderUnit.enabled = false;
             Destroy(gameObject,deathTime);
            //gameObject.SetActive(true);
        }
    }
}
