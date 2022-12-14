using System.Collections;
using UnityEngine;
using Assets.Scripts.AnimationsModel;
using Assets.Scripts.Enums;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Models
{
    public class MiningUnit : DefenceUnit
    {
        private float _softIncomeAmount;
        private float _softIncomeCooldown;

        [SerializeField] private ResourceModel _gunPowderPrefab;
        [SerializeField] private Transform _spawnDimond;
        [SerializeField] private AnimationModel _animationModel;

        private int _currentSoftIncomeTimer = 0;

        public override void Create()
        {
            base.Create();

            _currentHP = _unitData.GetCharacteristicData(CharacteristicUnitType.HP);
            _softIncomeAmount = _unitData.GetCharacteristicData(CharacteristicUnitType.Mining);
            _softIncomeCooldown = _unitData.GetCharacteristicData(CharacteristicUnitType.AbilityCooldown);

            _currentSoftIncomeTimer = 0;
            StartCoroutine(IncomeCoroutine());
        }

        public override void TakeDamage(float damage)
        {
            _currentHP -= damage;

            if (_currentHP <= 0)
            {
                PoolManager.Instance.GetParticleByType(ParticleType.Death_Type, gameObject.transform);
                Death();
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
                    PoolManager.Instance.GetResourceModelByType(_gunPowderPrefab.CurrencyType, _spawnDimond.transform).Setup(_softIncomeAmount, _gunPowderPrefab.CurrencyType);
                    _currentSoftIncomeTimer = 0;
                }

                _currentSoftIncomeTimer++;
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
