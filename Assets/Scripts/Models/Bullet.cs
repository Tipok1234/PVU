using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _speedBullet;
        [SerializeField] private float _bulletLifeTime;
        [SerializeField] private LayerMask _enemyLayer;

        private float _currentTimeBullet = 0;
        private float _damage;
        private bool _isActive;

        private void FixedUpdate()
        {
            if(_isActive)
            {
                //transform.Translate(_speedBullet * Time.deltaTime * Vector3.back);
                //transform.position += Vector3.back * _speedBullet * Time.deltaTime;

                var ray = new Ray(transform.position, transform.forward * (-0.15f));

                //Debug.DrawRay(transform.position, transform.forward * (-0.15f), Color.red, Time.deltaTime);

                if (Physics.Raycast(ray, out RaycastHit hit, 0.15f, _enemyLayer))
                {
                    if (hit.transform.TryGetComponent<AttackUnit>(out AttackUnit enemy))
                    {
                        ResetBullet();
                        enemy.TakeDamage(_damage);
                    }
                }

                if (_currentTimeBullet >= _bulletLifeTime)
                {
                    ResetBullet();
                }
                transform.position += Vector3.back * _speedBullet * Time.deltaTime;
                _currentTimeBullet += Time.deltaTime;
            }
        }

        public void Setup(float damage)
        {
            _isActive = true;
            _damage = damage;
        }


        private void ResetBullet()
        {
            _isActive = false;
            _currentTimeBullet = 0;
            gameObject.SetActive(false);
        }
    }
}
