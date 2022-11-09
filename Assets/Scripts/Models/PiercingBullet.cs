using UnityEngine;

namespace Assets.Scripts.Models
{
    public class PiercingBullet : Bullet
    {
        private void FixedUpdate()
        {
            if (_isActive)
            {
                var ray = new Ray(transform.position, transform.right * (-0.15f));

                if (Physics.Raycast(ray, out RaycastHit hit, 0.15f, _enemyLayer))
                {
                    if (hit.transform.TryGetComponent<AttackUnit>(out AttackUnit enemy))
                    {
                        enemy.TakeDamage(_damage);

                        if (_currentTimeBullet >= _bulletLifeTime)
                        {
                            ResetBullet();
                        }
                    }
                }

                if (Physics.Raycast(ray, out RaycastHit desctroyHit, 0.15f, _destoryBullet))
                {
                    ResetBullet();
                }

                transform.position += Vector3.right * _speedBullet * Time.deltaTime;
                _currentTimeBullet += Time.deltaTime;
            }
        }

        private void ResetBullet()
        {
            _isActive = false;
            _currentTimeBullet = 0;
            gameObject.SetActive(false);
        }
    }
}
