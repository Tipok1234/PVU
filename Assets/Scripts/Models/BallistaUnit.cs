using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.AnimationsModel;
using Assets.Scripts.Enums;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Models
{
    public class BallistaUnit : DefenceUnit
    {
        [SerializeField] private BulletType _bulletType;
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private Transform _spawnBullet;
        [SerializeField] private ParticleType _particleType;

        [SerializeField] private GameObject _gunModel;
        [SerializeField] private AnimationModel _animationModel;
        [SerializeField] private float _sphereRadius;

        private float _reloadTime = 0;
        private float _damage = 0;
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
                                PoolManager.Instance.GetBulletByType(_bulletType, _spawnBullet.transform).Setup(_damage, _attackUnit.transform.position - _gunModel.transform.position);
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
            PoolManager.Instance.GetParticleByType(_particleType, _spawnBullet);
        }

        public override void Create()
        {
            base.Create();

            _currentHP = _unitData.GetCharacteristicData(CharacteristicUnitType.HP);
            _damage = _unitData.GetCharacteristicData(CharacteristicUnitType.Damage);
            _reloadTime = _unitData.GetCharacteristicData(CharacteristicUnitType.AbilityCooldown);
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
    }
}
