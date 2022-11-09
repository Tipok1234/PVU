using UnityEngine;
using Assets.Scripts.Enums;
using Assets.Scripts.UI;
using Assets.Scripts.DataSo;

namespace Assets.Scripts.Managers
{
    public class SelectHandManager : MonoBehaviour
    {
        public SelectHandWindow SelectHandWindow => _selectHandWindow;

        [SerializeField] private SelectHandWindow _selectHandWindow;

        private DataManager _dataManager;

        private void Awake()
        {
            _selectHandWindow.SaveHandItemAction += OnSaveHandItem;
            _selectHandWindow.RemoveHandItemAction += OnRemoveHandItem;
        }

        public void Setup(DataManager dataManager)
        {
            _dataManager = dataManager;
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
