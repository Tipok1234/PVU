using System.Collections;
using UnityEngine;
using Assets.Scripts.AnimationsModel;
using Assets.Scripts.Enums;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Models
{
    public class DoubleShooterUnit : DefenceUnit
    {
        [SerializeField] private Bullet _bullet;
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private Transform _spawnBullet;
        [SerializeField] private ParticleType _particleType;

        [SerializeField] private float _reloadTime;
        [SerializeField] private float _reloadTimeBullet;
        [SerializeField] private float _damageUnit = 10;
        [SerializeField] private AnimationModel _animationModel;

        private float _currentReloadTime = 0;

        public override void Create()
        {
            base.Create();
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
                            _animationModel.PlayAnimation();
                            ShootParticle();
                            PoolManager.Instance.GetBulletByType(_bullet.BulletType, _spawnBullet.transform).Setup(_damageUnit, -transform.right);
                          //  Instantiate(_bullet, _spawnBullet.transform.position, _bullet.transform.rotation).Setup(_damageUnit, -transform.right);

                            yield return new WaitForSeconds(_reloadTimeBullet);

                            _animationModel.PlayAnimation();
                            ShootParticle();
                            PoolManager.Instance.GetBulletByType(_bullet.BulletType, _spawnBullet.transform).Setup(_damageUnit, -transform.right);
                           // Instantiate(_bullet, _spawnBullet.transform.position, _bullet.transform.rotation).Setup(_damageUnit, -transform.right);

                            _currentReloadTime = 0;
                        }
                    }
                }
                yield return null;
            }             
        }


        private void ShootParticle()
        {
            PoolManager.Instance.GetParticleByType(_particleType, _spawnBullet);
        }
        public override void TakeDamage(float damage)
        {
            _currentHP -= damage;

            if (_currentHP <= 0)
            {
                PoolManager.Instance.GetParticleByType(ParticleType.Death_Type, gameObject.transform);
                Death();
            }
        }

        public override void Death(float deathTime = 0)
        {
            StopCoroutine(LogicCoroutine());
            base.Death(deathTime);
           
        }
    }

}
