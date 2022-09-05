using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class BallistaUnit : DefenceUnit
    {
        [SerializeField] private Bullet _bullet;
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private Transform _spawnBullet;

        [SerializeField] private float _reloadTime;
        [SerializeField] private float _damageUnit = 10;

        [SerializeField] private GameObject _gunModel;

        private float _currentReloadTime = 0;
        private AttackUnit _attackUnit;

        private void FixedUpdate()
        {
            if (_isDead)
                return;

            if (_isActive)
            {
                if (_attackUnit != null && _attackUnit.IsDead == false)
                {
                    RotateTowardsEnemy();
                }

                _currentReloadTime += Time.deltaTime;
                if (_currentReloadTime >= _reloadTime)
                {

                    if (Physics.CheckSphere(transform.position, 10f, _enemyLayer))
                    {
                        var hitsInfo = Physics.SphereCastAll(transform.position, 10, transform.position, 10f, _enemyLayer);

                        float nearDistance = 10000f;

                        int targetIndex = 0;


                        for (int i = 0; i < hitsInfo.Length; i++)
                        {

                            var testDistance = Vector3.Distance(transform.position, hitsInfo[i].transform.position);

                            if (testDistance < nearDistance)
                                targetIndex = i;

                        }

                        if (hitsInfo[targetIndex].transform.TryGetComponent<AttackUnit>(out AttackUnit enemy))
                        {       
                            _attackUnit = enemy;
                            
                            Instantiate(_bullet, _spawnBullet.transform.position, _bullet.transform.rotation).Setup(_damageUnit);
                            _currentReloadTime = 0;         
                        }
                    }
                }             
            }
        }

        private void RotateTowardsEnemy()
        {
            Vector3 lookRotation = (_attackUnit.transform.position - _gunModel.transform.position);
            //_gunModel.transform.forward = lookRotation;
            _gunModel.transform.rotation = Quaternion.LookRotation(lookRotation);
        }

        void OnDrawGizmosSelected()
        {
            // Draw a yellow sphere at the transform's position
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, 10f);
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

            if (_hp <= 0)
            {
                Death();
            }
        }
    }
}
