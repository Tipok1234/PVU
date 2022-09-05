using UnityEngine;
using Assets.Scripts.AnimationsModel;

namespace Assets.Scripts.Models
{
    public class ShooterUnit : DefenceUnit
    {
        [SerializeField] private Bullet _bullet;
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private Transform _spawnBullet;

        [SerializeField] private float _reloadTime;
        [SerializeField] private float _damageUnit = 10;

        [SerializeField] private AnimationModel _animationModel;

        private float _currentReloadTime = 0;

        private void FixedUpdate()
        {
            if (_isDead)
                return;
            
            if(_isActive)
            {
                _currentReloadTime += Time.deltaTime;
                if (_currentReloadTime >= _reloadTime)
                {
                    var ray = new Ray(transform.position, transform.right * (-10));

                    if (Physics.Raycast(ray, out RaycastHit hit, 150f, _enemyLayer))
                    {
                        if (hit.transform.TryGetComponent<AttackUnit>(out AttackUnit enemy))
                        {
                            Instantiate(_bullet, _spawnBullet.transform.position, _bullet.transform.rotation).Setup(_damageUnit,-transform.right);
                            _animationModel.PlayAnimation();
                            _currentReloadTime = 0;
                        }
                    }
                }
            }

        }

        public override void Create()
        {
            base.Create();
        }

        public void CreateShooter()
        {

        }
        public override void TakeDamage(float damage)
        {
            _hp -= damage;

            if(_hp <= 0)
            {
                Death();
            }
        }
    }
}
