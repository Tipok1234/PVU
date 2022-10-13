using Assets.Scripts.Enums;
using UnityEngine;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Models
{
    public class ExplodingWallUnit : DefenceUnit
    {
       // [SerializeField] private ParticleSystem _deathParticle;
        [SerializeField] private LayerMask _enemyLayer;

        private float _damage;

        public override void Create()
        {
            base.Create();
            _currentHP = _unitData.GetCharacteristicData(CharacteristicUnitType.HP);
            _damage = _unitData.GetCharacteristicData(CharacteristicUnitType.Damage);
        }
        public override void TakeDamage(float damage)
        {
            _currentHP -= damage;

            if (_currentHP <= 0)
            {
                var rayCast = Physics.OverlapSphere(transform.position, 0.7f, _enemyLayer);

                for (int i = 0; i < rayCast.Length; i++)
                {
                    if (rayCast[i].transform.TryGetComponent<AttackUnit>(out AttackUnit e))
                    {
                        e.TakeDamage(_damage);
                    }
                }

                Death();
                PoolManager.Instance.GetParticleByType(ParticleType.Death_Type, gameObject.transform);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 0.7f);
        }
    }
}
