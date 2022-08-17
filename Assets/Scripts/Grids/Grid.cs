using Assets.Scripts.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Grids
{
    public class Grid : MonoBehaviour
    {

        private GridCell[] _gridCell;
        [SerializeField] private GameObject _selectGameUnit;
        [SerializeField] private GridCell _gridCellPrefab;
        [SerializeField] private List<GameUnitModel> _gameUnitModels;
        private Camera _mainCamera;

        private GameObject _gameUnit;

        private void Awake()
        {
            _mainCamera = Camera.main;
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

                    _gameUnit.transform.position = new Vector3(x, 0.25f, z);
                }

                if (Input.GetMouseButtonDown(0))
                {
                    if (Physics.Raycast(ray, out RaycastHit hitInfo))
                    {
                        if (hitInfo.transform.TryGetComponent<GridCell>(out GridCell gridCell))
                        {
                            if (gridCell.IsBusy)
                                return;


                            gridCell.PlaceUnit(_gameUnit.transform);
                            _gameUnit = null;
                        }
                    }
                }

                if (Input.GetMouseButtonDown(1))
                {
                    _gameUnit.SetActive(false);
                    _gameUnit = null;
                }
            }
        }
        public void Setup(int w, int l)
        {
            _gridCell = new GridCell[w * l];

            int k = 0;

            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < l; j++)
                {
                    var cell = Instantiate(_gridCellPrefab, new Vector3(i, 0, j), Quaternion.identity);
                    cell.SetCell(i, j);
                    cell.transform.parent = transform;

                    _gridCell[k] = cell;

                    k++;
                }
            }
        }

        public void StartPlaceUnit(UnitType unitType)
        {
            if (_gameUnit != null)
            {
                Destroy(_gameUnit);
            }

            for (int i = 0; i < _gameUnitModels.Count; i++)
            {
                if(unitType == _gameUnitModels[i].UnitType)
                {
                    _gameUnit = Instantiate(_gameUnitModels[i].UnitPrefab);
                    _gameUnit.SetActive(true);
                    break;
                }
            }
        }
    }

    [System.Serializable]
    public class GameUnitModel
    {
        public UnitType UnitType => _unitType;
        public GameObject UnitPrefab => _unitPrefab;
        public float PosY => _posY;

        [SerializeField] private UnitType _unitType;
        [SerializeField] private GameObject _unitPrefab;
        [SerializeField] private float _posY;
    }
}
