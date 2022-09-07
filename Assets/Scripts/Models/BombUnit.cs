using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.AnimationsModel;

namespace Assets.Scripts.Models
{
    public class BombUnit : DefenceUnit
    {
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private int _damage;
        [SerializeField] private float _explosionTime;
        [SerializeField] private AnimationModel _animationModel;
        public override void Create()
        {
            base.Create();
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
                    Debug.LogError(e.transform.name);
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
            base.Death(deathTime);
        }
    }
}
