using UnityEngine;
using System;
using System.Collections;

namespace Assets.Scripts.Models
{
    public class BaseUnit : MonoBehaviour
    {
        public event Action<BaseUnit> UnitDeadAction;
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
            _isDead = false;
            _isActive = true;
            _colliderUnit.enabled = true;
            _currentHP = _hp;
        }

        public virtual void Refresh()
        {
            _currentHP = _hp;
        }

        public virtual void Death(float deathTime = 0.5f)
        {
            _isDead = true;
            _isActive = false;
            _colliderUnit.enabled = false;
            StartCoroutine(DeathUnitCoroutine(deathTime));
        }

        protected virtual IEnumerator DeathUnitCoroutine(float deathTime = 0.5f)
        {
            yield return new WaitForSeconds(deathTime);

            UnitDeadAction?.Invoke(this);
            gameObject.SetActive(false);
            ResetUnit();
        }

        protected virtual void ResetUnit()
        {

        }
    }
}
