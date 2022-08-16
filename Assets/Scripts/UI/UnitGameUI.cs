using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Assets.Scripts.DataSo;

namespace Assets.Scripts.UIManager
{
    public class UnitGameUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private Button _buyButton;
        [SerializeField] private Image _unitImage;

        public event Action BuyUnitAction; 

        private UnitDataSo _unitDataSo;

        private void Awake()
        {
            _buyButton.onClick.AddListener(BuyButton);
        }
        public void Currncy(UnitDataSo unitDataSo)
        {
            _priceText.text = unitDataSo.Price.ToString();
            //image
            //method SETUP
        }

        public void BuyButton()
        {
            BuyUnitAction?.Invoke();
        }
    }
}
