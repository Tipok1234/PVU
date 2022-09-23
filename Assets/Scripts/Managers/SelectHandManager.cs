using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Enums;
using Assets.Scripts.UIManager;
using Assets.Scripts.DataSo;

namespace Assets.Scripts.Managers
{
    public class SelectHandManager : MonoBehaviour
    {
        [SerializeField] private SelectHandWindow _selectHandWindow;
        private DataManager _dataManager;

        private void Awake()
        {

            _selectHandWindow.SaveHandItemAction += OnSaveHandItem;
            _selectHandWindow.RemoveHandItemAction += OnRemoveHandItem;
        }

        public void Setup(UnitDataSo[] unitDataSo,DataManager dataManager)
        {
            _dataManager = dataManager;
            _selectHandWindow.Setup(unitDataSo,dataManager.UnitHandItems);
        }

        private void OnSaveHandItem(DefenceUnitType defenceUnitType)
        {
            _dataManager.SaveHandItem(defenceUnitType);
        }

        private void OnRemoveHandItem(DefenceUnitType defenceUnitType)
        {
            _dataManager.RemoveHandItem(defenceUnitType);
        }
    }
}
