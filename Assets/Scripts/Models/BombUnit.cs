using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.AnimationsModel;
using Assets.Scripts.Enums;
using Assets.Scripts.Managers;
using Assets.Scripts.DataSo;

namespace Assets.Scripts.Models
{
    public class BombUnit : DefenceUnit
    {
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private ParticleType _particleType;
        [SerializeField] private float _explosionTime;
        [SerializeField] private AnimationModel _animationModel;

        private float _damage = 0;

        public override void Create()
        {
            base.Create();      

            _damage = _unitData.GetCharacteristicData(CharacteristicUnitType.Damage);
            _explosionTime = _unitData.GetCharacteristicData(CharacteristicUnitType.AbilityCooldown);

            StartCoroutine(LogicBombCoroutine());

        }

        private IEnumerator LogicBombCoroutine()
        {
            _animationModel.PlayAnimation();

            yield return new WaitForSeconds(_explosionTime);

            var rayCast = Physics.OverlapSphere(transform.position, 1.5f, _enemyLayer);

            for (int i = 0; i < rayCast.Length; i++)
            {
                if (rayCast[i].transform.TryGetComponent<AttackUnit>(out AttackUnit e))
                {
                    e.TakeDamage(_damage);
                }
            }
            Death();

        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position,1.5f);
        }

        public override void Death(float deathTime = 0)
        {
            StopCoroutine(LogicBombCoroutine());
            PoolManager.Instance.GetParticleByType(_particleType, gameObject.transform);
            base.Death(deathTime);
        }
    }
}
