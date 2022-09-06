using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.AnimationsModel;

namespace Assets.Scripts.Models
{
    public class SpikeUnit : DefenceUnit
    {
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private int _damage;
        [SerializeField] private float _reloadTimeDamage;
        [SerializeField] private AnimationModel _animationModel;

        public override void Create()
        {
            base.Create();
            StartCoroutine(LogicSpikeCoroutine());
        }

        private IEnumerator LogicSpikeCoroutine()
        {

            while (true)
            {
                if (Physics.CheckBox(transform.position, transform.right * 1f, Quaternion.identity, _enemyLayer))
                {
                    Debug.DrawLine(transform.position, transform.right * 0.5f, Color.red, Time.deltaTime);

                    var rayCast = Physics.BoxCastAll(transform.position, transform.right * 1f, new Vector3(0f, 0.1f, 0), Quaternion.identity, 0.1f, _enemyLayer);

                    _animationModel.PlayAnimation();

                    for (int i = 0; i < rayCast.Length; i++)
                    {
                        if (rayCast[i].transform.TryGetComponent<AttackUnit>(out AttackUnit e))
                        {
                            Debug.LogError(e.transform.name);
                            e.TakeDamage(_damage);
                        }
                    }
                    
                }
                yield return new WaitForSeconds(2f);
            }
        }

        //private void OnDrawGizmos()
        //{
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawCube(transform.position, transform.localScale);
        //}

        //public override void TakeDamage(float damage)
        //{
        //    _hp -= damage;

        //    if (_hp <= 0)
        //    {
        //        Death();
        //    }
        //}

        public override void Death(float deathTime = 0)
        {
            StopCoroutine(LogicSpikeCoroutine());
            base.Death(deathTime);
        }
    }
}
