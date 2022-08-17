using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Controller;

namespace Assets.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        private int _width = 6;
        private int _length = 10;

       [SerializeField] private Grids.Grid _grid;

       [SerializeField] private GameUIController _gameUIController;

        private void Start()
        {
            _grid.Setup(_width,_length);
            _gameUIController.InstantiateUnit();
        }
    }
}
