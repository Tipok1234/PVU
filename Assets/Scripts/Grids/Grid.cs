using Assets.Scripts.Enums;
using System.Collections.Generic;
using System;
using Assets.Scripts.Models;
using UnityEngine;
using Assets.Scripts.Managers;
using Assets.Scripts.Controller;

namespace Assets.Scripts.Grids
{
    public class Grid : MonoBehaviour
    {
        public IReadOnlyList<Transform> EnemySpawnPoints => _enemySpawnPoints;
        public event Action<DefenceUnitType> UnitCreateAction;
        public event Action<DefenceUnitType> UnitSoldAction;
        public event Action<float,CurrencyType> CurrencyCollectedAction;

        [SerializeField] private LayerMask _gridCellLayer;

        [SerializeField] private GridCell _gridCellPrefab;
        [SerializeField] private GameUIController _gameUIController;

        private List<Transform> _enemySpawnPoints = new List<Transform>();
        private bool _isSell;

        private Camera _mainCamera;
        private GridCell[] _gridCell;
        private DefenceUnit _gameUnit;
        private static FieldBounes _fieldBounes;

        private void Awake()
        {
            _mainCamera = Camera.main;
            _gameUIController.SellButtonAction += SellButton;
        }

        private void Update()
        {

            if (_gameUnit != null)
            {
                var groundPlane = new Plane(Vector3.up, Vector3.zero);
                Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

                if (groundPlane.Raycast(ray, out float position))
                {
                    Vector3 worldPos = ray.GetPoint(position);

                    int x = Mathf.RoundToInt(worldPos.x);
                    int z = Mathf.RoundToInt(worldPos.z);

                    if(x <= _fieldBounes.MaxX && x>= _fieldBounes.MinX  && z <= _fieldBounes.MaxY && z >= _fieldBounes.MinY)
                    {
                        _gameUnit.gameObject.SetActive(true);
                    }
                    else
                    {
                        _gameUnit.gameObject.SetActive(false);
                    }

                    _gameUnit.transform.position = new Vector3(x, 0.25f, z);
                }

                if (Input.GetMouseButtonDown(0))
                {                 
                    if (Physics.Raycast(ray, out RaycastHit hitInfo, _gridCellLayer))
                    {
                        if (hitInfo.transform.TryGetComponent<GridCell>(out GridCell gridCell))
                        {

                            if (gridCell.IsBusy && gridCell.BaseUnit.CurrentHP <= 20)
                            {
                                if (_gameUnit.DefencUnitType != gridCell.BaseUnit.DefencUnitType)
                                    return;

                                gridCell.RegenurationUnit();
                                UnitCreateAction?.Invoke(_gameUnit.DefencUnitType);

                                _gameUnit.gameObject.SetActive(false);
                                _gameUnit = null;
                            }
                            else if (gridCell.IsBusy && gridCell.BaseUnit.CurrentHP > 20)
                            {
                                return;
                            }
                            else if (!gridCell.IsBusy)
                            {
                                gridCell.PlaceUnit(_gameUnit);
                                _gameUnit.Create();
                                UnitCreateAction?.Invoke(_gameUnit.DefencUnitType);
                                _gameUnit = null;
                            }
                        }
                    }
                }

                if (Input.GetMouseButtonDown(1))
                {
                    _gameUnit.gameObject.SetActive(false);
                    PoolManager.Instance.ReturnUnitToPool(_gameUnit.transform);
                    _gameUnit = null;
                }
            }

            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(ray, out RaycastHit hitInfo))
                    {
                        if (hitInfo.transform.TryGetComponent<ResourceModel>(out ResourceModel resourceModel))
                        {
                            CurrencyCollectedAction?.Invoke(resourceModel.CurrencyAmount, resourceModel.CurrencyType);
                            resourceModel.gameObject.SetActive(false);
                            return;
                        }
                        
                        if (_isSell && hitInfo.transform.TryGetComponent<DefenceUnit>(out DefenceUnit defenceUnit))
                        {
                            UnitSoldAction?.Invoke(defenceUnit.DefencUnitType);
                            defenceUnit.Death();
                            _isSell = false;
                        }
                    }
                }              
            }
        }

        public void SellButton()
        {
                _isSell = true;
        }
        public void Setup(int w, int l)
        {
            _gridCell = new GridCell[w * l];

            int k = 0;


            for (int i = 0; i < l; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    var cell = Instantiate(_gridCellPrefab, new Vector3(i, 0, j), Quaternion.identity);
                    cell.SetCell(i, j);
                    cell.transform.parent = transform;

                    cell.transform.name = k.ToString();

                    _gridCell[k] = cell;

                    k++;                                                     
                }
            }

            for (int i = 0; i < w; i++)
            {
                Vector3 cartPos = new Vector3(-1, 0.25f, i);

                PoolManager.Instance.GetCart(cartPos);             

                Vector3 enemyPos = new Vector3(l + 1, 0,i);
                Transform enemyPoint = new GameObject("SpawnPosition_" + i).transform;
                enemyPoint.position = enemyPos;
                enemyPoint.SetParent(transform);
                _enemySpawnPoints.Add(enemyPoint);
            }
            _fieldBounes = new FieldBounes(0, l, 0, w);
        }

        static public Vector3 GetXZFieldRandomVector()
        {
            return _fieldBounes.GetXZFieldRandomVector();
        }

        public void StartPlaceUnit(DefenceUnit defenceUnit)
        {
            _gameUnit = defenceUnit;
        }
    }
    public class FieldBounes
    {
        public float MinX => _minX;
        public float MaxX => _maxX;
        public float MinY => _minY;
        public float MaxY => _maxY;

        private float _minX = 0;
        private float _maxX = 0;
        private float _minY = 0;
        private float _maxY = 0;

        public FieldBounes(float mixX, float maxX, float minY, float maxY)
        {
            _minX = mixX;
            _maxX = maxX;
            _minY = minY;
            _maxY = maxY;
        }

        public Vector3 GetXZFieldRandomVector()
        {
            float randomX = UnityEngine.Random.Range(_minX, _maxX);
            float randomZ = UnityEngine.Random.Range(_minY, _maxY);
            return new Vector3(randomX, 0, randomZ);
        }
    }
}