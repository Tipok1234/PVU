using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Grids
{
    public class GridCell : MonoBehaviour
    {
        public int X => _x;
        public int Z => _z;
        public bool IsBusy => _isBusy;

        public void SetCell(int x, int z)
        {
            _x = x;
            _z = z;
        }

        [SerializeField] private GameObject _hightLight;

        private int _x;
        private int _z;
        private bool _isBusy;

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
