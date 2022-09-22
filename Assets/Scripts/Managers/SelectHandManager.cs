using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Enums;
using Assets.Scripts.UIManager;

namespace Assets.Scripts.Managers
{
    public class SelectHandManager : MonoBehaviour
    {
        [SerializeField] private SelectHandWindow _selectHandWindow;
        private DataManager _dataManager;

        //private string _unitHandItemsKey = "UnitHandItemsKey";

        //private List<DefenceUnitType> _unitHandItems;

        private void Awake()
        {
            _selectHandWindow.SaveHandItemAction += OnSaveHandItem;
            _selectHandWindow.RemoveHandItemAction += OnRemoveHandItem;
        }
        private void Start()
        {
            _dataManager = FindObjectOfType<DataManager>();
        }

        public void OnSaveHandItem(DefenceUnitType defenceUnitType)
        {
            _dataManager.SaveHandItem(defenceUnitType);
            Debug.LogError("SELECTHAND !!!! - " + _dataManager.UnitHandItems.Count);
            //_dataManager.Save(_unitHandItemsKey, _unitHandItems);

            //_unitHandItems = new List<DefenceUnitType>();
            //_unitHandItems.Add(defenceUnitType);
        }

        public void OnRemoveHandItem(DefenceUnitType defenceUnitType)
        {

            _dataManager.RemoveHandItem(defenceUnitType);

            Debug.LogError("SELECTHAND DELETE !!!! - " + _dataManager.UnitHandItems.Count);
            //_dataManager.Save(_unitHandItemsKey, _unitHandItems);

            //_unitHandItems.Remove(defenceUnitType);
        }
    }
}
