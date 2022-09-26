using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.AnimationsModel;

namespace Assets.Scripts.Models
{
    public class BallistaUnit : DefenceUnit
    {
        [SerializeField] private Bullet _bullet;
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private Transform _spawnBullet;
        [SerializeField] private ParticleSystem _shootParticle;
        [SerializeField] private ParticleSystem _deathParticle;

        [SerializeField] private float _reloadTime;
        [SerializeField] private float _damageUnit = 10;

        [SerializeField] private GameObject _gunModel;
        [SerializeField] private AnimationModel _animationModel;

        [SerializeField] private float _sphereRadius;
        private float _currentReloadTime = 0;
        private AttackUnit _attackUnit;

        private void FixedUpdate()
        {
            if (_isDead)
                return;

            if (_isActive)
            {
                if (_attackUnit != null && _attackUnit.IsDead == false)
                {
                    RotateTowardsEnemy();
                }

                _currentReloadTime += Time.deltaTime;
                if (_currentReloadTime >= _reloadTime)
                {
                    if (Physics.CheckSphere(transform.position, _sphereRadius, _enemyLayer))
                    {
                        var hitsInfo = Physics.OverlapSphere(transform.position, _sphereRadius, _enemyLayer);

                        float nearDistance = 10000f;

                        int targetIndex = 0;


                        for (int i = 0; i < hitsInfo.Length; i++)
                        {

                            float testDistance = Vector3.Distance(transform.position, hitsInfo[i].transform.position);

                            if (testDistance < nearDistance)
                            {
                                nearDistance = testDistance;
                                targetIndex = i;
                            }
                        }

                        if (targetIndex < hitsInfo.Length)
                        {
                            if (hitsInfo[targetIndex].transform.TryGetComponent<AttackUnit>(out AttackUnit enemy))
                            {
                                _attackUnit = enemy;

                                _animationModel.PlayAnimation();
                                ShootParticle();
                                Instantiate(_bullet, _spawnBullet.transform.position, _bullet.transform.rotation).Setup(_damageUnit, _attackUnit.transform.position - _gunModel.transform.position);
                                _currentReloadTime = 0;
                            }
                        }
                    }
                }             
            }
        }

        private void RotateTowardsEnemy()
        {
            Vector3 lookRotation = (_attackUnit.transform.position - _gunModel.transform.position);
            _gunModel.transform.rotation = Quaternion.LookRotation(lookRotation);
        }

        private void ShootParticle()
        {
            Instantiate(_shootParticle, _spawnBullet);
        }

        //void OnDrawGizmosSelected()
        //{
        //    // Draw a yellow sphere at the transform's position
        //    Gizmos.color = Color.yellow;
        //    Gizmos.DrawSphere(transform.position, 12f);
        //}

        public override void Create()
        {
            base.Create();
        }
        public override void TakeDamage(float damage)
        {
            _currentHP -= damage;

            if (_currentHP <= 0)
            {
                Death();
                Death();
                _deathParticle.transform.position = gameObject.transform.position;
                var particleSystem = Instantiate(_deathParticle);
                Destroy(particleSystem, 2f);
            }
        }
    }
}
