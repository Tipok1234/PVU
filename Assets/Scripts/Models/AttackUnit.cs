using UnityEngine;
using System.Collections;
using Assets.Scripts.Enums;
using System;

namespace Assets.Scripts.Models
{
    public class AttackUnit : BaseUnit
    {
        new public event Action<AttackUnit> UnitDeadAction;
        public AttackUnitType AttackUnitType => _attackUnitType;

        [SerializeField] private AttackUnitType _attackUnitType;
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

        public override void Create()
        {
            _isDead = false;
            _isActive = true;
            base.Create();
        }

        protected override IEnumerator DeathUnitCoroutine(float deathTime = 0.5f)
        {
            Debug.LogError("1");
            //yield return new WaitForSeconds(3f);
            UnitDeadAction?.Invoke(this);
            yield return base.DeathUnitCoroutine(deathTime);
            Debug.LogError("2");

            //gameObject.SetActive(false);
        }
        //private IEnumerator LogicEnemyCoroutine()
        //{
        // gameObject.SetActive(false);

        //    
        //}
        public void DeathUnit()
        {
            _isDead = true;
            _animator.SetTrigger("Death");
            _isWalk = false;
            _isAttack = false;
            _animator.SetBool("Walk", false);
            _animator.SetBool("Attack", false);
            _colliderUnit.enabled = false;

            //StartCoroutine(DeathUnitCoroutine(3f));


            base.Death(3f);
            //UnitDeadAction?.Invoke();
            //_isDead = true;
            //_isActive = false;
            //gameObject.SetActive(true);
            // base.Death(3.0f);
        }

        public override void TakeDamage(float damage)
        {
            _currentHP -= damage;

            if (_currentHP <= 0)
            {
                DeathUnit();
            }
        }

        public void TakeDamageDebuff(float damage, BaseDebuff baseDebuff)
        {
            switch (baseDebuff.DebuffType)
            {
                case DebuffType.Frost_Debuff:
                    FrostDebuff fd = baseDebuff as FrostDebuff;

                    break;
                case DebuffType.Stun_Debuff:

                    break;
            }

            _currentHP -= damage;
        }
    }
}
