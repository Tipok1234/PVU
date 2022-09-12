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


        [SerializeField] private TMP_Text _priceBuyUnitText;
        [SerializeField] private Button _selectUnitButton;

        private DefenceUnitType _defenceUnitType;
        private void Awake()
        {
            _priceBuyUnitText.text = 10.ToString();
            _selectUnitButton.onClick.AddListener(SelectUnitUI);
        }

        public void Setup(UnitDataSo unitDataSO)
        {
            _defenceUnitType = unitDataSO.DefencUnitType;
            _unitImage.sprite = unitDataSO.UnitSprite;
            _openImage.enabled = !unitDataSO.IsOpen;
            _priceBuyUnitText.enabled = !unitDataSO.IsOpen;
        }
        public void OpenUnit()
        {
            _openImage.enabled = false;
            _priceBuyUnitText.gameObject.SetActive(false);
        }

        public void SelectUnitUI()
        {
            SelectUnitAction?.Invoke(_defenceUnitType);
        }
    }
}
