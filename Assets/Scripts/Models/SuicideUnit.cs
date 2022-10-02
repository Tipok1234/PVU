using System.Collections;
using UnityEngine;
using Assets.Scripts.AnimationsModel;
using Assets.Scripts.Enums;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Models
{
    public class SuicideUnit : DefenceUnit
    {
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private AnimationModel _animationModel;
        [SerializeField] private ParticleType _particleType;

        private float _damage;

        public override void Create()
        {
            base.Create();

            _damage = _unitData.GetCharacteristicData(CharacteristicUnitType.Damage);

            StartCoroutine(LogicSuisideCoroutine());
        }

        private IEnumerator LogicSuisideCoroutine()
        {
            _animationModel.PlayAnimation();

            var p1 = PoolManager.Instance.GetParticleByType(_particleType, transform.GetChild(0));
            var p2 = PoolManager.Instance.GetParticleByType(_particleType, transform.GetChild(1));

            p1.SetParent(transform.GetChild(0));
            p2.SetParent(transform.GetChild(1));

            p1.position = gameObject.transform.position;
            p2.position = gameObject.transform.position;

            var rayCast = Physics.RaycastAll(transform.position + transform.right * (-10f), transform.right * (20f), _enemyLayer);

            for (int i = 0; i < rayCast.Length; i++)
            {
                if (rayCast[i].transform.TryGetComponent<AttackUnit>(out AttackUnit enemy))
                {
                    enemy.TakeDamage(_damage);
                }
            }

            yield return new WaitForSeconds(0.7f);

            PoolManager.Instance.ReturnToPool(p1);
            PoolManager.Instance.ReturnToPool(p2);

            Death();
        }
        public override void Death(float deathTime = 0)
        {
            StopCoroutine(LogicSuisideCoroutine());
            base.Death(deathTime);
        }
    }
}

