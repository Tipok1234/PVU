using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.AnimationsModel;
using Assets.Scripts.Enums;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Models
{
    public class FrostShooterUnit : DefenceUnit
    {
        [SerializeField] private BulletType _bulletType;
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private Transform _spawnBullet;

        private float _reloadTime = 0;
        private float _damage = 0;

        [SerializeField] private AnimationModel _animationModel;
        [SerializeField] private ParticleType _particleType;

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

                            PoolManager.Instance.GetBulletByType(_bulletType, _spawnBullet.transform).Setup(_damage, -transform.right,frostDebuff);
                            ShootParticle();
                            enemy.TakeDamage(_damage);
                            _animationModel.PlayAnimation();
                            _currentReloadTime = 0;
                        }
                    }
                }
            }
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
