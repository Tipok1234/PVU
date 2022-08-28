using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class ShooterUnit : DefenceUnit
    {
        [SerializeField] private Bullet _bullet;
       
        [SerializeField] private float _reloadTime;
        [SerializeField] private LayerMask _enemyLayer;

        private float _currentReloadTime = 0;
        [SerializeField] private float _damageUnit = 10;

        private bool _isTarget;

        private void FixedUpdate()
        {
            if (_isDead)
                return;


            _currentReloadTime += Time.deltaTime;
            if (_currentReloadTime >= _reloadTime)
            {
                var ray = new Ray(transform.position, transform.right * (-10));

               // Debug.DrawRay(transform.position, transform.right * (- 10f),Color.red,3.0f);

                if (Physics.Raycast(ray, out RaycastHit hit, 150f, _enemyLayer))
                {
                    if (hit.transform.TryGetComponent<AttackUnit>(out AttackUnit enemy))
                    {
                        Instantiate(_bullet, transform.position, _bullet.transform.rotation).Setup(_damageUnit);
                        _currentReloadTime = 0;
                    }
                }
            }
        }

        public override void TakeDamage(float damage)
        {
            Debug.LogError(_hp);
            _hp -= damage;

            if(_hp <= 0)
            {
                Death();
            }
        }

        public override void Death()
        {
            _isDead = true;
            _colliderUnit.enabled = false;
            Destroy(gameObject);
        }
    }
}
