using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.AnimationsModel;
using Assets.Scripts.Enums;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Models
{
    public class SuicideUnit : DefenceUnit
    {
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private int _damage;
        [SerializeField] private float _explosionTime;
        [SerializeField] private AnimationModel _animationModel;
        [SerializeField] private ParticleType _particleType;

        public override void Create()
        {
            base.Create();
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

            //var particleSystem = Instantiate(_particleSystem, transform.GetChild(0));
            //var particleSystem_1 = Instantiate(_particleSystem, transform.GetChild(1));

            yield return new WaitForSeconds(0.2f);

            var ray = new Ray(transform.position + transform.right * (-10f), transform.right * (20f));

            if (Physics.Raycast(ray, out RaycastHit hit, 20f, _enemyLayer))
            {
                var rayCast = Physics.RaycastAll(transform.position + transform.right * (-10f), transform.right * (20f), _enemyLayer);

                for (int i = 0; i < rayCast.Length; i++)
                {

                    if (rayCast[i].transform.TryGetComponent<AttackUnit>(out AttackUnit enem))
                    {
                        enem.TakeDamage(_damage);

                    }
                }
            }
            yield return new WaitForSeconds(0.5f);

            PoolManager.Instance.ReturnToPool(p1);
            PoolManager.Instance.ReturnToPool(p2);

            Death();
            //Destroy(particleSystem, 2f);
            //Destroy(particleSystem_1, 2f);
        }

        //protected override void ResetUnit()
        //{
        //    transform.GetChild(0).position = new Vector3(0f,0.2f,0f);
        //    transform.GetChild(1).position = new Vector3(0f, 0.2f, 0f);
        //}

        public override void Death(float deathTime = 0)
        {
            StopCoroutine(LogicSuisideCoroutine());
            base.Death(deathTime);
          //  ResetUnit();
        }
    }
}

