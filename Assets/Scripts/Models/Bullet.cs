using UnityEngine;

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

        private void FixedUpdate()
        {
            if(_isActive)
            {
                var ray = new Ray(transform.position, transform.right * (0.15f));

                Debug.DrawRay(transform.position, transform.right * (0.15f), Color.red, Time.deltaTime);

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
                transform.position += Vector3.right * _speedBullet * Time.deltaTime;
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
