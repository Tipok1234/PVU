using UnityEngine;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Models
{
    public class ResourceModel : MonoBehaviour
    {
        public int CurrencyAmount => _currencyAmount;
        public CurrencyType CurrencyType => _currencyType;

        private int _currencyAmount;
        private CurrencyType _currencyType;

        public void Setup(int currency, CurrencyType currencyType)
        {
            _currencyType = currencyType;
            _currencyAmount = currency;
            Destroy(gameObject, 3.0f);       
        }
    }
}
