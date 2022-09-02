using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Models;
using Assets.Scripts.DataSo;

namespace Assets.Scripts.Managers
{

    public class LevelManager : MonoBehaviour
    {
        public LevelDataSO[] Levels => _levels;

        [SerializeField] private LevelDataSO[] _levels;

        public LevelDataSO GetLevelByIndex(int index)
        {
            Debug.LogError(index);
            if (index >= _levels.Length)
            {
                Debug.LogError("Wrong Level Index");
                return _levels[0];
            }

            return _levels[index];
        }
    }
}