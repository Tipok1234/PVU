using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Scripts.Grids;

namespace Assets.Scripts.Models
{
    public class MiningUnit : DefenceUnit
    {
        public GunPowderModel GunPowderModel => _gunPowderPrefab;

        [SerializeField] private int _softIncomeAmount;
        [SerializeField] private float _softIncomeCooldown;
        [SerializeField] private GunPowderModel _gunPowderPrefab;
        [SerializeField] private Transform _spawnDimond;
        private GridCell _gridCell;

        private const float _yPos = 4;
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
        public override void Death()
        {
            //base.Death();
            _isDead = true;
            _colliderUnit.enabled = false;
            Destroy(gameObject);
            StopCoroutine(IncomeCoroutine());
        }

        private IEnumerator IncomeCoroutine()
        {
            while(true)
            {
                if (_currentSoftIncomeTimer >= _softIncomeCooldown)
                {
                    //Vector3 newPos = Grids.Grid.GetXZFieldRandomVector();
                    //newPos.y = _yPos;

                    Instantiate(_gunPowderPrefab, _spawnDimond.transform.position, Quaternion.identity);
                    _currentSoftIncomeTimer = 0;
                }

                _currentSoftIncomeTimer++;
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
