using UnityEngine;
using Assets.Scripts.Enums;
using System.Collections;

namespace Assets.Scripts.Models
{
    public class ResourceModel : MonoBehaviour
    {
        public int CurrencyAmount => _currencyAmount;
        public CurrencyType CurrencyType => _currencyType;

        private int _currencyAmount;

        [SerializeField] private CurrencyType _currencyType;

        public void Setup(int currency, CurrencyType currencyType)
        {
            _currencyType = currencyType;
            _currencyAmount = currency;
            StartCoroutine(ResourceCoroutine()); 
        }

        private IEnumerator ResourceCoroutine()
        {
            yield return new WaitForSeconds(2f);
            gameObject.SetActive(false);
        }
    }
}
