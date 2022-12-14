using System.Collections;
using Assets.Scripts.AnimationsModel;
using Assets.Scripts.Enums;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class MineUnit : DefenceUnit
    {
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private AnimationMine _animationMane;
        [SerializeField] private ParticleType _particleType;

        private float _damage;
        private float _timeDamage;

        private void FixedUpdate()
        {
            if (_isDead)
                return;
        }

        public override void Create()
        {
            base.Create();

            _damage = _unitData.GetCharacteristicData(CharacteristicUnitType.Damage);
            _timeDamage = _unitData.GetCharacteristicData(CharacteristicUnitType.AbilityCooldown);
            _currentHP = _unitData.GetCharacteristicData(CharacteristicUnitType.HP);

            StartCoroutine(LogicMineCoroutine());
        }
        private IEnumerator LogicMineCoroutine()
        {
            yield return new WaitForSeconds(_timeDamage);
            _animationMane.PlayAnimation();

            while(true)
            {
                var ray = new Ray(transform.position, transform.right * (0.6f));

                Debug.DrawRay(transform.position, transform.right * (0.6f), Color.red, Time.deltaTime);

                if (Physics.Raycast(ray, out RaycastHit hit, 0.6f, _enemyLayer))
                {                    
                    if (hit.transform.TryGetComponent<AttackUnit>(out AttackUnit enemy))
                    {
                        var rayCast = Physics.RaycastAll(transform.position, transform.right, 1f, _enemyLayer);

                        for (int i = 0; i < rayCast.Length; i++)
                        {
                            if(rayCast[i].transform.TryGetComponent<AttackUnit>(out AttackUnit e))
                            {
                                e.TakeDamage(_damage);                
                            }
                        }
                        PoolManager.Instance.GetParticleByType(_particleType, gameObject.transform);
                        Death();
                    }
                }
                yield return new WaitForFixedUpdate();
            }
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
            StopCoroutine(LogicMineCoroutine());
            base.Death(deathTime);         
        }
    }
}
