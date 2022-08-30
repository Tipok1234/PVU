using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts.Models
{
    public class ShooterUnit : DefenceUnit
    {
        [SerializeField] private Bullet _bullet;
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private Transform _spawnBullet;

        [SerializeField] private float _reloadTime;
        [SerializeField] private float _damageUnit = 10;

        private float _currentReloadTime = 0;

        private void FixedUpdate()
        {
            if (_isDead)
                return;


            _currentReloadTime += Time.deltaTime;
            if (_currentReloadTime >= _reloadTime)
            {
                var ray = new Ray(transform.position, transform.right * (-10));

                if (Physics.Raycast(ray, out RaycastHit hit, 150f, _enemyLayer))
                {
                    if (hit.transform.TryGetComponent<AttackUnit>(out AttackUnit enemy))
                    {
                        Instantiate(_bullet, _spawnBullet.transform.position, _bullet.transform.rotation).Setup(_damageUnit);
                        _currentReloadTime = 0;
                    }
                }
            }
        }

        private void TransformShooter()
        {
            gameObject.transform.DOMove(new Vector3(2, 0, 0),2);
        }

        public override void TakeDamage(float damage)
        {
            _hp -= damage;

            if(_hp <= 0)
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
