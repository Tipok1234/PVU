using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.DataSo;
using Assets.Scripts.Enums;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Assets.Scripts.UIManager
{
    public class ShopWindow : MonoBehaviour
    {
        [SerializeField] private Button _buyButton;

        [SerializeField] private Image _unitImage;
        [SerializeField] private TMP_Text _softCurrencyText;
        [SerializeField] private TMP_Text _hardCurrencyText;
        [SerializeField] private TMP_Text _priceUnitText;
        [SerializeField] private TMP_Text _damageText;
        [SerializeField] private TMP_Text _healthText;
        [SerializeField] private TMP_Text _coolDownText;

        private void Awake()
        {
            _buyButton.onClick.AddListener(BuyUnitGame);
        }

        public void Setup(UnitDataSo unitDataSo)
        {
            _unitImage.sprite = unitDataSo.UnitSprite;
            _priceUnitText.text = unitDataSo.Price.ToString();
            
            _healthText.text = unitDataSo.HP.ToString();
            _coolDownText.text = unitDataSo.PlaceCooldown.ToString();

            _softCurrencyText.text = CurrencyType.SoftCurrency.ToString();
            _hardCurrencyText.text = CurrencyType.HardCurrency.ToString();
        }

        private void BuyUnitGame()
        {

        }
    }
}
