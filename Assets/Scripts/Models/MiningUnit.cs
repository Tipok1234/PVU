using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class MiningUnit : DefenceUnit
    {
        [SerializeField] private int _softIncomeAmount;
        [SerializeField] private float _softIncomeCooldown;
        [SerializeField] private GameObject _gunPowderPrefab;


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
        public override void Death()
        {
            base.Death();
            StopCoroutine(IncomeCoroutine());
        }

        private IEnumerator IncomeCoroutine()
        {
            while(true)
            {
                if (_currentSoftIncomeTimer >= _softIncomeCooldown)
                {
                    Vector3 newPos = Grids.Grid.GetXZFieldRandomVector();
                    newPos.y = _yPos;

                    Instantiate(_gunPowderPrefab, newPos , Quaternion.identity);
                    _currentSoftIncomeTimer = 0;
                }

                _currentSoftIncomeTimer++;
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
