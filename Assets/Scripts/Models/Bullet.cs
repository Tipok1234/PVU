using UnityEngine;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Models
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] protected float _speedBullet;
        [SerializeField] protected float _bulletLifeTime;
        [SerializeField] protected LayerMask _enemyLayer;

        protected float _currentTimeBullet = 0;
        protected float _damage;
        protected bool _isActive;

        private Vector3 _direction;
        private BaseDebuff _baseDebuff;

        private void FixedUpdate()
        {
            if(_isActive)
            {
                transform.position += _direction * _speedBullet * Time.deltaTime;
                _currentTimeBullet += Time.deltaTime;

                var ray = new Ray(transform.position, _direction * (0.15f));

                Debug.DrawRay(transform.position, _direction * (0.15f), Color.red, Time.deltaTime);

                if (Physics.Raycast(ray, out RaycastHit hit, 0.15f, _enemyLayer))
                {
                    if (hit.transform.TryGetComponent<AttackUnit>(out AttackUnit enemy))
                    {
                        if(_baseDebuff == null)
                        {
                            enemy.TakeDamage(_damage);
                        }
                        else
                        {
                            enemy.TakeDamageDebuff(_damage,_baseDebuff);
                        }
                        
                        ResetBullet();
                        return;
                    }
                }

                if (_currentTimeBullet >= _bulletLifeTime)
                {
                    ResetBullet();
                }


            }
        }

        public void Setup(float damage, Vector3 direction)
        {
            _direction = direction.normalized;
            _direction.y = 0;
            _damage = damage;
            _isActive = true;
            
        }

        public void Setup(float damage, Vector3 direction, BaseDebuff baseDebuff)
        {
            _baseDebuff = baseDebuff;
            _direction = direction.normalized;
            _direction.y = 0;
            _damage = damage;
            _isActive = true;

        }


        private void ResetBullet()
        {
            _isActive = false;
            _currentTimeBullet = 0;
            gameObject.SetActive(false);
        }
    }
}
