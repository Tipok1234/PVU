using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class AttackUnit : BaseUnit
    {
        [SerializeField] private Transform _selfTransform;

        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _damage;
        [SerializeField] private Animator _animator;

        private bool _isWalk = true;
        private bool _isAttack = true;
        private bool _isDeath;

        private void FixedUpdate()
        {

            if (_isDeath)
                return;

            WalkUnit();
            DeathUnit();
        }
        public void WalkUnit()
        {
            if (_isWalk)
            {
                _selfTransform.transform.position += Vector3.forward * _moveSpeed * Time.deltaTime;
                _animator.SetBool("Walk", true);
                _animator.SetBool("Attack", false);
            }
            else
            {
                _animator.SetBool("Walk", false);
                _animator.SetBool("Attack", true);
            }
        }
        private void DeathUnit()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                _isAttack = true;
                _isWalk = false;
            }

            if (Input.GetKey(KeyCode.KeypadEnter))
            {
                _isWalk = true;
                _isAttack = false;        
               
            }

            if (Input.GetKey(KeyCode.W))
            {
                _animator.SetTrigger("Death");
                _isDeath = true;
                _isWalk = false;
                _isAttack = false;
                _animator.SetBool("Walk", false);
                _animator.SetBool("Attack", false);

            }
        }
    }
}
