using UnityEngine;
using Assets.Scripts.DataSo;
using Assets.Scripts.Enums;
using UnityEngine.UI;
using System;

namespace Assets.Scripts.UI
{
    public class ShowUnitUIItem : MonoBehaviour
    {
        public event Action<ShowUnitUIItem> SelectHandUnitAction;
        public DefenceUnitType DefenceUnitType => _defenceUnitType;
        public Sprite UnitShowImage => _unitImage.sprite;
        public bool IsOpenImage => _isOpenImage;

        [SerializeField] private Button _selectUnitButton;
        [SerializeField] private Image _unitImage;
        [SerializeField] private Image _lockImage;

        private DefenceUnitType _defenceUnitType;
        private bool _isOpenImage;

        private void Awake()
        {
            _selectUnitButton.onClick.AddListener(SelectUnit);
        }

        public void Setup(UnitDataSo unitDataSo)
        {
            _defenceUnitType = unitDataSo.DefencUnitType;
            _unitImage.sprite = unitDataSo.UnitSprite;
            _defenceUnitType = unitDataSo.DefencUnitType;
            _isOpenImage = unitDataSo.IsOpen;

            if (unitDataSo.IsOpen)
            {
                _lockImage.enabled = false;
            }
            else
            {
                _lockImage.enabled = true;
            }

        }

        public void SelectUnit()
        {
            SelectHandUnitAction?.Invoke(this);
        }
    }
}
