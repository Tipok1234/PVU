using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.AnimationsModel;

namespace Assets.Scripts.Models
{
    public class SuicideUnit : DefenceUnit
    {
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private int _damage;
        [SerializeField] private float _explosionTime;
        [SerializeField] private AnimationModel _animationModel;

        private void FixedUpdate()
        {
            if (_isDead)
                return;
        }

        public override void Create()
        {
            base.Create();
            StartCoroutine(LogicSuisideCoroutine());
        }

        private IEnumerator LogicSuisideCoroutine()
        {

            _animationModel.PlayAnimation();
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
                        Death();
                    }
                }
            }
        }

        //private void OnDrawGizmos()
        //{
        //    Debug.DrawRay(transform.position + transform.right * (-10f), transform.right * (20f), Color.red, Time.deltaTime);
        //}

        public override void Death(float deathTime = 0)
        {
            StopCoroutine(LogicSuisideCoroutine());
            base.Death(deathTime);
        }


    }

}

