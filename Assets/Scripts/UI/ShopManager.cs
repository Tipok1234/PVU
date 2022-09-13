using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Managers;

namespace Assets.Scripts.UIManager
{
    public class ShopManager : MonoBehaviour
    {
        [SerializeField] private ShopManager _shopManager;

        private DataManager _dataManager;

        private void Awake()
        {
            _dataManager = FindObjectOfType<DataManager>();
        }
    }
}
