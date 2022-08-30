using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Scripts.Grids;
using Assets.Scripts.Controller;

namespace Assets.Scripts.Models
{
    public class MiningUnit : DefenceUnit
    {

        [SerializeField] private int _softIncomeAmount;
        [SerializeField] private float _softIncomeCooldown;
        [SerializeField] private GunPowderModel _gunPowderPrefab;
        [SerializeField] private Transform _spawnDimond;
        [SerializeField] private AnimationUnit _animationUnit;
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
            base.Death();
            StopCoroutine(IncomeCoroutine());
            //_isDead = true;
            //_colliderUnit.enabled = false;
            //Destroy(gameObject);
        }

        private IEnumerator IncomeCoroutine()
        {
            while(true)
            {
                if (_currentSoftIncomeTimer >= _softIncomeCooldown)
                {
                    //Vector3 newPos = Grids.Grid.GetXZFieldRandomVector();
                    //newPos.y = _yPos;
                    _animationUnit.AnimationMining();
                    Instantiate(_gunPowderPrefab, _spawnDimond.transform.position, Quaternion.identity).Setup();
                    _currentSoftIncomeTimer = 0;
                }

                _currentSoftIncomeTimer++;
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
