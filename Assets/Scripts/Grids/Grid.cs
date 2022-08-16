using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Grids
{
    public class Grid : MonoBehaviour
    {
        private GridCell[] _gridCell;

        [SerializeField] private GridCell _gridCellPrefab;

        public void Setup(int w, int l)
        {
            _gridCell = new GridCell[w * l];

            int k = 0;

            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < l; j++)
                {

                    var cell = Instantiate(_gridCellPrefab,new Vector3(i,0,j),Quaternion.identity);
                    cell.SetCell(i, j);
                    _gridCell[k] = cell;

                    cell.transform.parent = transform;
                    _gridCellPrefab = cell;

                    k++;
                }
            }
        }
    }
}
