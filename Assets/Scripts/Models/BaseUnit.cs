using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Grids;
using System;

namespace Assets.Scripts.Models
{
    public class BaseUnit : MonoBehaviour
    {
        public event Action UnitDeadAction;

        public static BaseUnit Instance = instance;
        public float HP => _hp;
        public Collider ColliderUnit => _colliderUnit;

        [SerializeField] protected float _hp;
        [SerializeField] protected Collider _colliderUnit;

        private static BaseUnit instance;
        


        protected bool _isDead = false;
        protected bool _isActive = false;

        private void Awake()
        {
            if (instance == null)
                instance = this;  
        }
        public virtual void TakeDamage(float damage)
        {
            _hp -= damage;
        }

        public virtual void Create()
        {
            
        }

        public virtual void Death()
        {
            UnitDeadAction?.Invoke();
            _isDead = true;
            _colliderUnit.enabled = false;
            Destroy(gameObject);
        }
    }
}
