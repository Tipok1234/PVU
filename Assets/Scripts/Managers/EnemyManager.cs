using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private Transform[] _transformUnit;
        [SerializeField] private GameObject[] _unitPrefab;
    }
}
