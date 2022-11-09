using UnityEngine;
using System;
using UnityEngine.UI;
using Assets.Scripts.Enums;

namespace Assets.Scripts.UI
{
    public class HandItem : MonoBehaviour
    {
        public event Action<DefenceUnitType> DeleteUnitHandActioon;
        public DefenceUnitType DefenceUnitType => _defenceUnitType;

        [SerializeField] private Button _deleteUnitHandButton;
        public bool IsBusy => _isBusy;
        private bool _isBusy;
        private DefenceUnitType _defenceUnitType;
        private Transform _tranform;

        private void Awake()
        {
            _deleteUnitHandButton.onClick.AddListener(DeleteUnit);
        }

        public void SetBusy(bool isBusy, DefenceUnitType defenceUnitType = default, Transform parent = null)
        {
            _isBusy = isBusy;
            _tranform = parent;
            _defenceUnitType = defenceUnitType;
        }

        private void DeleteUnit()
        {
            ResetElement();

            DeleteUnitHandActioon?.Invoke(_defenceUnitType);
        }

        public void ResetElement()
        {
            if (_isBusy == true)
            {
                _isBusy = false;
                Destroy(_tranform.gameObject);
            }
        }
    }
}
