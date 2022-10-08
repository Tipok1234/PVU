using System.Collections;
using UnityEngine;
using Assets.Scripts.AnimationsModel;
using Assets.Scripts.Enums;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Models
{
    public class DoubleShooterUnit : DefenceUnit
    {
        [SerializeField] private BulletType _bulletType;
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private Transform _spawnBullet;
        [SerializeField] private ParticleType _particleType;

        [SerializeField] private float _timeBetweenBullets = 0.2f;
        [SerializeField] private AnimationModel _animationModel;

        private float _currentReloadTime = 0;
        private float _reloadTime = 0;
        private float _damage = 0;

        public override void Create()
        {
            base.Create();
            StartCoroutine(LogicCoroutine());

            _currentHP = _unitData.GetCharacteristicData(CharacteristicUnitType.HP);
            _damage = _unitData.GetCharacteristicData(CharacteristicUnitType.Damage);
            _reloadTime = _unitData.GetCharacteristicData(CharacteristicUnitType.AbilityCooldown);
        }
        private IEnumerator LogicCoroutine()
        {
            while (true)
            {
                _currentReloadTime += Time.deltaTime;

                if (_currentReloadTime >= _reloadTime)
                {
                    var ray = new Ray(transform.position, transform.right * (-10));

                    if (Physics.Raycast(ray, 150f, _enemyLayer))
                    {

                        _animationModel.PlayAnimation();
                        ShootParticle();
                        PoolManager.Instance.GetBulletByType(_bulletType, _spawnBullet.transform).Setup(_damage, -transform.right);

                        yield return new WaitForSeconds(_timeBetweenBullets);

                        _animationModel.PlayAnimation();
                        ShootParticle();
                        PoolManager.Instance.GetBulletByType(_bulletType, _spawnBullet.transform).Setup(_damage, -transform.right);

                        _currentReloadTime = 0;

                    }
                }
                yield return null;
            }
        }


        private void ShootParticle()
        {
            AudioManager.Instance.PlaySoundGame(AudioSoundType.ShootSound);
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
