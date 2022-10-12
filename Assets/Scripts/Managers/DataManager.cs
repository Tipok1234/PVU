using UnityEngine;
using Assets.Scripts.Enums;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Assets.Scripts.Managers
{
    public class DataManager : MonoBehaviour
    {
        public IReadOnlyDictionary<DefenceUnitType, int> UnitsDictionary => _unitsDictionary;

        public event Action<float, CurrencyType> UpdateCurrencyAction;
        public List<DefenceUnitType> UnitHandItems => _unitHandItems;
        public int LevelIndex => _levelIndex;
        public float SoftCurrency => _softCurrency;
        public float HardCurrency => _hardCurrency;
        public static DataManager Instance => _instance;

        private int _levelIndex;
        private float _softCurrency;
        private float _hardCurrency;

        private string _levelKey = "Level";
        private string _softCurrencyKey = "SoftCurrency";
        private string _hardCurrencyKey = "HardCurrency";
        private string _defencesUnitsUpgradeKey = "DefencesUnitsUpgradeKey";
        private string _unitHandItemsKey = "UnitHandItemsKey";

        private static DataManager _instance;

        private List<DefenceUnitType> _unitHandItems;


        private Dictionary<DefenceUnitType, int> _unitsDictionary;


        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        public void LoadData()
        {
            _softCurrency = PlayerPrefs.GetFloat(_softCurrencyKey, 2500);
            _hardCurrency = PlayerPrefs.GetFloat(_hardCurrencyKey, 250);
            _levelIndex = PlayerPrefs.GetInt(_levelKey, 0);

            _unitsDictionary = Load<Dictionary<DefenceUnitType, int>>(_defencesUnitsUpgradeKey);
            _unitHandItems = Load<List<DefenceUnitType>>(_unitHandItemsKey);

            if (_unitsDictionary.Count == 0)
            {
                _unitsDictionary = new Dictionary<DefenceUnitType, int>();
                _unitsDictionary.Add(DefenceUnitType.Mining, 0);
                _unitsDictionary.Add(DefenceUnitType.Shooter, 0);
                _unitsDictionary.Add(DefenceUnitType.Mine, 0);
            }

            UpdateCurrencyAction?.Invoke(_softCurrency, CurrencyType.SoftCurrency);
            UpdateCurrencyAction?.Invoke(_hardCurrency, CurrencyType.HardCurrency);
        }

        public bool CheckCurrency(int currencyAmount, CurrencyType currencyType)
        {
            switch (currencyType)
            {
                case CurrencyType.SoftCurrency:

                    return _softCurrency >= currencyAmount;

                case CurrencyType.HardCurrency:

                    return _hardCurrency >= currencyAmount;
            }
            return false;
        }



        public void RemoveCurrency(int currencyAmount, CurrencyType currencyType)
        {
            switch (currencyType)
            {
                case CurrencyType.SoftCurrency:
                    _softCurrency -= currencyAmount;
                    PlayerPrefs.SetFloat(_softCurrencyKey, _softCurrency);
                    UpdateCurrencyAction?.Invoke(_softCurrency, currencyType);
                    break;
                case CurrencyType.HardCurrency:
                    _hardCurrency -= currencyAmount;
                    PlayerPrefs.SetFloat(_hardCurrencyKey, _hardCurrency);
                    UpdateCurrencyAction?.Invoke(_hardCurrency, currencyType);
                    break;
            }
        }
        public void UpdateLevel()
        {
            _levelIndex++;
            PlayerPrefs.SetInt(_levelKey, _levelIndex);
        }

        public void AddCurrency(float currencyAmount, CurrencyType currencyType)
        {
            switch (currencyType)
            {
                case CurrencyType.SoftCurrency:
                    _softCurrency += currencyAmount;
                    PlayerPrefs.SetFloat(_softCurrencyKey, _softCurrency);
                    UpdateCurrencyAction?.Invoke(_softCurrency, currencyType);
                    break;
                case CurrencyType.HardCurrency:
                    _hardCurrency += currencyAmount;
                    PlayerPrefs.SetFloat(_hardCurrencyKey, _hardCurrency);
                    UpdateCurrencyAction?.Invoke(_hardCurrency, currencyType);
                    break;
            }          
        }

        public void BuyUnit(DefenceUnitType defenceUnitType)
        {
            if (_unitsDictionary.ContainsKey(defenceUnitType))
                return;

            _unitsDictionary.Add(defenceUnitType, 0);
            Debug.LogError(JsonConvert.SerializeObject(_unitsDictionary));

            Save(_defencesUnitsUpgradeKey, _unitsDictionary);

        }

        public void SaveHandItem(DefenceUnitType defenceUnitType)
        {
            if (_unitHandItems.Contains(defenceUnitType))
                return;

            _unitHandItems.Add(defenceUnitType);

            Save(_unitHandItemsKey, _unitHandItems);
        }

        public void RemoveHandItem(DefenceUnitType defenceUnitType)
        {
            if (_unitHandItems.Contains(defenceUnitType))
            {
                _unitHandItems.Remove(defenceUnitType);

                Save(_unitHandItemsKey, _unitHandItems);
            }
        }

        public void LevelUpUnit(DefenceUnitType defenceUnitType)
        {
            var unitDictionary = _unitsDictionary.ContainsKey(defenceUnitType);

            if (unitDictionary == false)
                return;

            _unitsDictionary[defenceUnitType]++;

            Save(_defencesUnitsUpgradeKey, _unitsDictionary);
        }


        private void Save<T>(string key, T saveData)
        {
            string jsonDataString = JsonConvert.SerializeObject(saveData);
            PlayerPrefs.SetString(key, jsonDataString);
        }

        private T Load<T>(string key) where T : new()
        {
            if (PlayerPrefs.HasKey(key))
            {
                string loadedString = PlayerPrefs.GetString(key);
                return JsonConvert.DeserializeObject<T>(loadedString);
            }
            else
            {
                return new T();
            }
        }
    }
}
