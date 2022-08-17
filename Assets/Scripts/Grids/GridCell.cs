using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Grids
{
    public class GridCell : MonoBehaviour
    {
        public Transform SpawnPoint => _spawnPoint;
        public int X => _x;
        public int Z => _z;
        public bool IsBusy => _isBusy;

        private int _x;
        private int _z;
        private bool _isBusy;

        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private GameObject _hightLight;

        public void SetCell(int x, int z)
        {
            _x = x;
            _z = z;
        }

        public void PlaceUnit(Transform unit)
        {
            unit.SetParent(_spawnPoint);
            unit.localPosition = Vector3.zero;

            _isBusy = true;
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
