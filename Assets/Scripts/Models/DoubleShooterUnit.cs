using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class DoubleShooterUnit : DefenceUnit
    {
        [SerializeField] private Bullet _bullet;
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private Transform _spawnBullet;

        [SerializeField] private float _reloadTime;
        [SerializeField] private float _reloadTimeBullet;
        [SerializeField] private float _damageUnit = 10;

        private float _currentReloadTime = 0;

        private void FixedUpdate()
        {
            if (_isDead)
                return;
        }

        public override void Create()
        {
            StartCoroutine(LogicCoroutine());
        }
        private IEnumerator LogicCoroutine()
        {
            while (true)
            {
                _currentReloadTime += Time.deltaTime;

                if (_currentReloadTime >= _reloadTime)
                {
                    var ray = new Ray(transform.position, transform.right * (-10));

                    if (Physics.Raycast(ray, out RaycastHit hit, 150f, _enemyLayer))
                    {
                        if (hit.transform.TryGetComponent<AttackUnit>(out AttackUnit enemy))
                        {
                            Instantiate(_bullet, _spawnBullet.transform.position, _bullet.transform.rotation).Setup(_damageUnit);

                            yield return new WaitForSeconds(_reloadTimeBullet);

                            Instantiate(_bullet, _spawnBullet.transform.position, _bullet.transform.rotation).Setup(_damageUnit);

                            _currentReloadTime = 0;
                        }
                    }
                }
                yield return null;
            }             
        }

        public override void TakeDamage(float damage)
        {
            Debug.LogError(_hp);
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
            StopCoroutine(LogicCoroutine());
            Destroy(gameObject);
        }
    }

}
