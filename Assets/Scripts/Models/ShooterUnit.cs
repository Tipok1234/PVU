using UnityEngine;
using Assets.Scripts.AnimationsModel;
using Assets.Scripts.Managers;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Models
{
    public class ShooterUnit : DefenceUnit
    {
        [SerializeField] private Bullet _bullet;
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private Transform _spawnBullet;

        [SerializeField] private ParticleType _particleType;

        [SerializeField] private float _reloadTime;
        [SerializeField] private float _damageUnit = 10;

        [SerializeField] private AnimationModel _animationModel;

        private float _currentReloadTime = 0;

        private void FixedUpdate()
        {
            if (_isDead)
                return;
            
            if(_isActive)
            {
                _currentReloadTime += Time.deltaTime;
                if (_currentReloadTime >= _reloadTime)
                {
                    var ray = new Ray(transform.position, transform.right * (-10));

                    if (Physics.Raycast(ray, out RaycastHit hit, 150f, _enemyLayer))
                    {
                        if (hit.transform.TryGetComponent<AttackUnit>(out AttackUnit enemy))
                        {
                            PoolManager.Instance.GetBulletByType(_bullet.BulletType, _spawnBullet.transform).Setup(_damageUnit, -transform.right);
                            ShootParticle();
                            _animationModel.PlayAnimation();
                            _currentReloadTime = 0;
                        }
                    }
                }
            }

        }
        public override void Create()
        {
            base.Create();
        }

        private void ShootParticle()
        {
            PoolManager.Instance.GetParticleByType(_particleType, _spawnBullet);
        }
        public void CreateShooter()
        {

        }
        public override void TakeDamage(float damage)
        {
            _currentHP -= damage;

            if(_currentHP <= 0)
            {
                PoolManager.Instance.GetParticleByType(ParticleType.Death_Type, gameObject.transform);
                Death();
            }
        }
    }
}
