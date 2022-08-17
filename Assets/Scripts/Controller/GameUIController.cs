using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.DataSo;
using Assets.Scripts.UIManager;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Controller
{
    public class GameUIController : MonoBehaviour
    {
        [SerializeField] private UnitDataSo[] _unitDataSo;
        [SerializeField] private UnitGameUI _unitGameUIPrefab;
        [SerializeField] private Transform _spawnParent;

        [SerializeField] private Grids.Grid _grid;

        private List<UnitGameUI> _unitGameUIList = new List<UnitGameUI>();

        public void InstantiateUnit()
        {
            ResetUI();

            for (int i = 0; i < _unitDataSo.Length; i++)
            {
                UnitGameUI unitUI = Instantiate(_unitGameUIPrefab, _spawnParent);
                unitUI.Setup(_unitDataSo[i]);
                unitUI.BuyUnitAction += OnBuyUnit;

                _unitGameUIList.Add(unitUI);
            }            
        }

        private void ResetUI()
        {
            for (int i = 0; i < _unitGameUIList.Count; i++)
            {
                _unitGameUIList[i].BuyUnitAction -= OnBuyUnit;
            }
            _unitGameUIList.Clear();
        }
        private void OnBuyUnit(UnitType unitType)
        {
            _grid.StartPlaceUnit(unitType);
        }
    }
}
