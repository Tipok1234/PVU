using UnityEngine;
using Assets.Scripts.AnimationsModel;
using Assets.Scripts.Managers;
using Assets.Scripts.Enums;
using Assets.Scripts.DataSo;

namespace Assets.Scripts.Models
{
    public class ShooterUnit : DefenceUnit
    {
        [SerializeField] private BulletType _bulletType;
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private Transform _spawnBullet;

        [SerializeField] private ParticleType _particleType;

        [SerializeField] private AnimationModel _animationModel;

        private float _currentReloadTime = 0;

        private float _damage = 0;
        private float _reloadTime = 0;


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

                    if (Physics.Raycast(ray, 150f, _enemyLayer))
                    {
                        PoolManager.Instance.GetBulletByType(_bulletType, _spawnBullet.transform).Setup(_damage, -transform.right);
                        ShootParticle();
                        _animationModel.PlayAnimation();
                        _currentReloadTime = 0;
                    }

                    //if (Physics.Raycast(ray, out RaycastHit hit, 150f, _enemyLayer))
                    //{
                    //    PoolManager.Instance.GetBulletByType(_bulletType, _spawnBullet.transform).Setup(_damageUnit, -transform.right);
                    //    ShootParticle();
                    //    _animationModel.PlayAnimation();
                    //    _currentReloadTime = 0;
                    //}
                }
            }

        }
        public override void Create()
        {
            base.Create();

            _currentHP = _unitData.GetCharacteristicData(CharacteristicUnitType.HP);
            _damage = _unitData.GetCharacteristicData(CharacteristicUnitType.Damage);
            _reloadTime = _unitData.GetCharacteristicData(CharacteristicUnitType.AbilityCooldown);

            Debug.LogError($"Type: {_unitDefenceType}, HP: {_currentHP}, DAMAGE: {_damage}, RELOAD: {_reloadTime}");
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
    }
}
