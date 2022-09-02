using System.Collections;
using UnityEngine;
using Assets.Scripts.AnimationsModel;

namespace Assets.Scripts.Models
{
    public class MiningUnit : DefenceUnit
    {

        [SerializeField] private int _softIncomeAmount;
        [SerializeField] private float _softIncomeCooldown;
        [SerializeField] private GunPowderModel _gunPowderPrefab;
        [SerializeField] private Transform _spawnDimond;
        [SerializeField] private AnimationModel _animationModel;

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
            _hp -= damage;

            if (_hp <= 0)
            {
                Death();
            }
        }
        public override void Death(float deathTime = 0)
        {
            base.Death(deathTime);
            StopCoroutine(IncomeCoroutine());
        }

        private IEnumerator IncomeCoroutine()
        {
            while(true)
            {
                if (_currentSoftIncomeTimer >= _softIncomeCooldown)
                {
                    _animationModel.PlayAnimation();
                    Instantiate(_gunPowderPrefab, _spawnDimond.transform.position, Quaternion.identity).Setup();
                    _currentSoftIncomeTimer = 0;
                }

                _currentSoftIncomeTimer++;
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
