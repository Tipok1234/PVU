using Assets.Scripts.Models;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Grids
{
    public class GridCell : MonoBehaviour
    {
        public static event Action<DefenceUnitType> UnitCreateAction;
      //  public static event Action<DefenceUnitType> UnitSoldAction;
        public DefenceUnit BaseUnit => _baseUnit;
        public Transform SpawnPoint => _spawnPoint;
        public int X => _x;
        public int Z => _z;
        public bool IsBusy => _isBusy;

        private int _x;
        private int _z;
        private bool _isBusy;

        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private GameObject _hightLight;

        private DefenceUnit _baseUnit;

        private void OnMouseDown()
        {
            if (Grid.GameUnit == null)
                return;

            if (!_isBusy)
            {
                PlaceUnit(Grid.GameUnit);
                Grid.GameUnit.Create();
                UnitCreateAction?.Invoke(Grid.GameUnit.DefencUnitType);
                // Grid.GameUnit = null;
            }
            else if(_isBusy && _baseUnit.CurrentHP <= 20)
            {
                if (Grid.GameUnit.DefencUnitType != _baseUnit.DefencUnitType)
                    return;

                RegenurationUnit();
                UnitCreateAction?.Invoke(Grid.GameUnit.DefencUnitType);
            }


            //if(Grid.IsSell && Grid.GameUnit != null)
            //{
            //    UnitSoldAction?.Invoke(Grid.GameUnit.DefencUnitType);
            //    Grid.GameUnit.Death();
            //}
        }

        public void SetCell(int x, int z)
        {
            _x = x;
            _z = z;
        }

        public void PlaceUnit(DefenceUnit unit)
        {
            _baseUnit = unit;
            _baseUnit.transform.SetParent(_spawnPoint);
            _baseUnit.transform.localPosition = Vector3.zero;
            _baseUnit.UnitDeadAction += OnUnitDead;

            _isBusy = true;
        }

        public void RegenurationUnit()
        {
            _baseUnit.Refresh();
        }

        private void SetEmpty()
        {
            _isBusy = false;
        }
        private void OnUnitDead(BaseUnit baseUnit)
        {
            _baseUnit.UnitDeadAction -= OnUnitDead;
            SetEmpty();
        }

        private void OnMouseEnter()
        {
            _hightLight.SetActive(true);
        }
        private void OnMouseExit()
        {
            _hightLight.SetActive(false);
        }
    }
}
