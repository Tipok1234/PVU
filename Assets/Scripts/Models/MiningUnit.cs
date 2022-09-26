using System.Collections;
using UnityEngine;
using Assets.Scripts.AnimationsModel;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Models
{
    public class MiningUnit : DefenceUnit
    {
        [SerializeField] private int _softIncomeAmount;
        [SerializeField] private float _softIncomeCooldown;
        [SerializeField] private ResourceModel _gunPowderPrefab;
        [SerializeField] private Transform _spawnDimond;
        [SerializeField] private AnimationModel _animationModel;
        [SerializeField] private ParticleSystem _deathParticle;
        [SerializeField] private CurrencyType _currencyType;

        private float _currentSoftIncomeTimer = 0;

        private void Start()
        {
            StartCoroutine(IncomeCoroutine());
            _currentSoftIncomeTimer = 0;
        }
        public override void Create()
        {
            base.Create();

            _currentSoftIncomeTimer = 0;
            StartCoroutine(IncomeCoroutine());
        }

        public override void TakeDamage(float damage)
        {
            _currentHP -= damage;

            if (_currentHP <= 0)
            {
                Death();
                _deathParticle.transform.position = gameObject.transform.position;
                var particleSystem = Instantiate(_deathParticle);
                Destroy(particleSystem, 2f);
            }
        }
        public override void Death(float deathTime = 0)
        {
            StopCoroutine(IncomeCoroutine());
            base.Death(deathTime);
        }

        private IEnumerator IncomeCoroutine()
        {
            while(true)
            {
                if (_currentSoftIncomeTimer >= _softIncomeCooldown)
                {
                    _animationModel.PlayAnimation();
                    Instantiate(_gunPowderPrefab, _spawnDimond.transform.position, Quaternion.identity).Setup(_softIncomeAmount, _currencyType);
                    _currentSoftIncomeTimer = 0;
                }

                _currentSoftIncomeTimer++;
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
