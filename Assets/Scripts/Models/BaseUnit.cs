using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class BaseUnit : MonoBehaviour
    {
        public float HP => _hp;
        public Collider ColliderUnit => _colliderUnit;

        [SerializeField] protected float _hp;
        [SerializeField] protected Collider _colliderUnit;

        private void Awake()
        {

        }

        public void TakeDamage(float damage)
        {
            _hp -= damage;
        }
    }
}
