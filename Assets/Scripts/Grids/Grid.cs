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
        public static bool IsSell => _isSell;
        public static DefenceUnit GameUnit => _gameUnit;

        [SerializeField] private LayerMask _gridCellLayer;

        [SerializeField] private GridCell _gridCellPrefab;
        [SerializeField] private GameUIController _gameUIController;

        private List<Transform> _enemySpawnPoints = new List<Transform>();
        private static bool _isSell;

        private Camera _mainCamera;
        private GridCell[] _gridCell;
        private static DefenceUnit _gameUnit;
        private static FieldBounes _fieldBounes;

        private void Awake()
        {
            _mainCamera = Camera.main;
            _gameUIController.SellButtonAction += SellButton;
        }

        private void Start()
        {
            GridCell.UnitCreateAction += OnUnitCreated;
            DefenceUnit.UnitSoldAction += OnUnitSold;
            ResourceModel.CurrencyCollectedAction += OnCurrencyCollected;
        }

        private void OnDestroy()
        {
            GridCell.UnitCreateAction -= OnUnitCreated;
            DefenceUnit.UnitSoldAction -= OnUnitSold;
            ResourceModel.CurrencyCollectedAction -= OnCurrencyCollected;
        }

        private void OnCurrencyCollected(float amount, CurrencyType currencyType)
        {
            CurrencyCollectedAction?.Invoke(amount,currencyType);
        }
        private void OnUnitCreated(DefenceUnitType defenceUnitType)
        {
            UnitCreateAction?.Invoke(_gameUnit.DefencUnitType);
            _gameUnit = null;
        }

        private void OnUnitSold(DefenceUnitType defenceUnitType)
        {
            UnitSoldAction?.Invoke(defenceUnitType);
            _isSell = false;
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
            if (_gameUnit != null)
            {
                _gameUnit.gameObject.SetActive(false);
                PoolManager.Instance.ReturnUnitToPool(_gameUnit.transform);
                _gameUnit = null;
            }

            _gameUnit = defenceUnit;
        }

        private void SellButton()
        {
            _isSell = true;
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