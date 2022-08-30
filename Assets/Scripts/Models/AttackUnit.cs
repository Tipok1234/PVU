using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class AttackUnit : BaseUnit
    {
        [SerializeField] private Transform _selfTransform;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _reloadTime;
        [SerializeField] private float _damage;
        [SerializeField] private Animator _animator;
        [SerializeField] private LayerMask _allyLayer;

        private float _currentReloadTime = 0;
        

        private bool _isWalk = true;
        private bool _isAttack = true;
        private bool _isTarget = true;

        private void FixedUpdate()
        {
            if (_isDead)
                return;


            var ray = new Ray(transform.position, transform.forward * (0.5f));

            Debug.DrawRay(transform.position, transform.forward * (0.5f), Color.red, Time.deltaTime);

            if (Physics.Raycast(ray, out RaycastHit hit, 0.5f, _allyLayer))
            {
                AttackerUnit();

                if (_currentReloadTime >= _reloadTime && hit.transform.TryGetComponent<DefenceUnit>(out DefenceUnit ally))
                {
                    ally.TakeDamage(_damage);
                    _currentReloadTime = 0;
                }
            }
            else
            {
                WalkUnit();
            }

            _currentReloadTime += Time.deltaTime;
        }
        public void WalkUnit()
        {
                _selfTransform.transform.position += Vector3.right * (-1f) * _moveSpeed * Time.deltaTime;
                _animator.SetBool("Walk", true);
                _animator.SetBool("Attack", false);

        }
        private void AttackerUnit()
        {

            _animator.SetBool("Walk", false);
            _animator.SetBool("Attack", true);
        }

        public void DeathUnit()
        {
            _isDead = true;
            _animator.SetTrigger("Death");
            _isWalk = false;
            _isAttack = false;
            _animator.SetBool("Walk", false);
            _animator.SetBool("Attack", false);
            _colliderUnit.enabled = false;
            Destroy(gameObject, 3f);
        }

        public override void TakeDamage(float damage)
        {
            _hp -= damage;

            if (_hp <= 0)
            {
                DeathUnit();
            }
        }
    }
}
