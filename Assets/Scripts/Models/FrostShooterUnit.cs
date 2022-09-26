using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.AnimationsModel;

namespace Assets.Scripts.Models
{
    public class FrostShooterUnit : DefenceUnit
    {
        [SerializeField] private Bullet _bullet;
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private Transform _spawnBullet;

        [SerializeField] private float _reloadTime;
        [SerializeField] private float _damageUnit = 10;

        [SerializeField] private AnimationModel _animationModel;
        [SerializeField] private ParticleSystem _shootParticle;
        [SerializeField] private ParticleSystem _deathParticle;

        private float _currentReloadTime = 0;

        private void FixedUpdate()
        {
            if (_isDead)
                return;

            if (_isActive)
            {
                _currentReloadTime += Time.deltaTime;
                if (_currentReloadTime >= _reloadTime)
                {
                    var ray = new Ray(transform.position, transform.right * (-10));

                    if (Physics.Raycast(ray, out RaycastHit hit, 150f, _enemyLayer))
                    {
                        if (hit.transform.TryGetComponent<AttackUnit>(out AttackUnit enemy))
                        {
                            FrostDebuff frostDebuff = new FrostDebuff(0.5f,1.5f);
                            Instantiate(_bullet, _spawnBullet.transform.position, _bullet.transform.rotation).Setup(_damageUnit, -transform.right, frostDebuff);
                            ShootParticle();
                            _animationModel.PlayAnimation();
                            _currentReloadTime = 0;
                        }
                    }
                }
            }

        }

        private void ShootParticle()
        {
            Instantiate(_shootParticle, _spawnBullet);
        }
        public override void Create()
        {
            base.Create();
        }

        public void CreateShooter()
        {

        }
        public override void TakeDamage(float damage)
        {
            _currentHP -= damage;

            if (_currentHP <= 0)
            {
                Death();
                _deathParticle.transform.position = gameObject.transform.position;
                var particleSystem = Instantiate(_deathParticle);
                Destroy(particleSystem, 2f);
            }
        }
    }
}
