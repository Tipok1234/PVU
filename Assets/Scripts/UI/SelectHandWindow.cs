using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.DataSo;

namespace Assets.Scripts.UIManager
{
    public class SelectHandWindow : MonoBehaviour
    {
        [SerializeField] private Transform _spawnHandUnitUI;
        [SerializeField] private Transform _spawnUnitImage;
        [SerializeField] private Transform _spawnShowInit;


        public void Setup(UnitDataSo[] unitDataSO)
        {
            for (int i = 0; i < unitDataSO.Length; i++)
            {

            }
        }
    }
}
