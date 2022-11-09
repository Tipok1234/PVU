using UnityEngine;
using System.Collections;
using Assets.Scripts.Enums;
using Assets.Scripts.DataSo;
using System;

namespace Assets.Scripts.Models
{
    public class AttackUnit : BaseUnit
    {
        new public event Action<AttackUnit> UnitDeadAction;
        public AttackUnitSO AttackUnitSO => _attackUnitSO;
        public bool IsAuraImmunity => _isAuraImmunity;

        [SerializeField] protected AttackUnitSO _attackUnitSO;
        [SerializeField] protected Transform _selfTransform;
        [SerializeField] private Animator _animator;
        [SerializeField] private LayerMask _allyLayer;
        [SerializeField] private SkinnedMeshRenderer _renderer;
        private float _currentReloadTime = 0;
        protected float _speedUnit;

        private bool _isWalk = true;
        private bool _isAttack = true;
        private bool _isTarget = true;
        private bool _isStop = false;

        [SerializeField] private bool _isImmunity;
        [SerializeField] protected bool _isAuraImmunity;

        private DefenceUnit _target;
        private void Start()
        {
            _speedUnit = _attackUnitSO.MoveSpeed;
            _currentHP = _attackUnitSO.HP;
        }
        protected virtual void FixedUpdate()
        {
            MoveUnit();
        }

        protected virtual void MoveUnit()
        {
            if (_isDead)
                return;

            var ray = new Ray(transform.position, transform.forward * (0.5f));

            if (Physics.Raycast(ray, out RaycastHit hit, 0.5f, _allyLayer))
            {

                if (_currentReloadTime >= _attackUnitSO.ReloadTimeAttack && hit.transform.TryGetComponent<DefenceUnit>(out DefenceUnit ally))
                {
                    _target = ally;
                    AttackerUnit();
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
            _selfTransform.transform.position += Vector3.right * (-1f) * _speedUnit * Time.deltaTime;
            _animator.SetBool("Walk", true);
            _animator.SetBool("Attack", false);
        }
        private void AttackerUnit()
        {
            _animator.SetBool("Walk", false);
            _animator.SetBool("Attack", true);
        }

        private void AttackAnimationCallback()
        {
            if(_target != null)
            {
                _target.TakeDamage(_attackUnitSO.Damage);
            }
        }

        public override void Create()
        {
            _isDead = false;
            _isActive = true;
            base.Create();
        }

        protected override IEnumerator DeathUnitCoroutine(float deathTime = 0.5f)
        {
            UnitDeadAction?.Invoke(this);
            yield return base.DeathUnitCoroutine(deathTime);
        }

        public void DeathUnit()
        {
            _isDead = true;
            _target = null;
            _animator.SetTrigger("Death");
            _isWalk = false;
            _isAttack = false;
            _animator.SetBool("Walk", false);
            _animator.SetBool("Attack", false);
            _colliderUnit.enabled = false;
            ResetBuffUnit();

            base.Death(3f);
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

        public void BuffUnit(DebuffType debuffType)
        {
            if (_isImmunity)
                return;

            switch (debuffType)
            {
                case DebuffType.Frost_Debuff:
                    {
                        var propBlock = new MaterialPropertyBlock();
                        _renderer.GetPropertyBlock(propBlock);
                        propBlock.SetColor("_Color", Color.blue);
                        _renderer.SetPropertyBlock(propBlock);
                        _speedUnit = 0.4f;
                        break;
                    }
                case DebuffType.Poison_Debuff:
                    {
                        var propBlock = new MaterialPropertyBlock();
                        _renderer.GetPropertyBlock(propBlock);
                        propBlock.SetColor("_Color", Color.green);
                        _renderer.SetPropertyBlock(propBlock);
                        break;
                    }
            }
        }


        public void TakeSkill()
        {
            var propBlock = new MaterialPropertyBlock();
            _renderer.GetPropertyBlock(propBlock);
            propBlock.SetColor("_Color", Color.blue);
            _renderer.SetPropertyBlock(propBlock);
            _speedUnit = 0f;
        }

        public void ReturnToUnit()
        {
            ResetBuffUnit();
            _speedUnit = _attackUnitSO.MoveSpeed;
        }

        private void ResetBuffUnit()
        {
            var propBlock = new MaterialPropertyBlock();
            _renderer.GetPropertyBlock(propBlock);

            propBlock.SetColor("_Color", Color.white);

            _renderer.SetPropertyBlock(propBlock);
        }

        public void AuraSpeed(float auraValue)
        {
            _speedUnit = _speedUnit * auraValue + _speedUnit;
        }

        public virtual void RetrunSpeedUnit()
        {
            _speedUnit = _attackUnitSO.MoveSpeed;
        }
    }
}
