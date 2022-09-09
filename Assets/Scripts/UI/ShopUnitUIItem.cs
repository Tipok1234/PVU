using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using Assets.Scripts.Enums;
using Assets.Scripts.DataSo;

namespace Assets.Scripts.UIManager
{
    public class ShopUnitUIItem : MonoBehaviour
    {
        public event Action<DefenceUnitType> SelectUnitAction;

        [SerializeField] private Image _unitImage;
        [SerializeField] private Image _openImage;
        [SerializeField] private Button _selectUnitButton;

        private DefenceUnitType _defenceUnitType;
        private void Awake()
        {
            _selectUnitButton.onClick.AddListener(SelectUnitUI);
        }

        public void Setup(UnitDataSo unitDataSO)
        {
            _defenceUnitType = unitDataSO.DefencUnitType;
            _unitImage.sprite = unitDataSO.UnitSprite;
            _openImage.enabled = !unitDataSO.IsOpen;
        }
        public void OpenUnit()
        {
            _openImage.enabled = false;
        }
        public void SelectUnitUI()
        {
            SelectUnitAction?.Invoke(_defenceUnitType);
        }
    }
}
