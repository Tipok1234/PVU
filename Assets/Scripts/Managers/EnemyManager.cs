using Assets.Scripts.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private Transform[] _spawnPositions;
        [SerializeField] private GameObject[] _unitPrefabs;


        private void Start()
        {
            Setup();
        }

        public void Setup()
        {
            for (int i = 0; i < _unitPrefabs.Length; i++)
            {
                Instantiate(_unitPrefabs[i], _spawnPositions[i]);
            }
        }
    }
}
