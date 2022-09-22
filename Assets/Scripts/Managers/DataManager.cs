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

        public List<DefenceUnitType> UnitHandItems => _unitHandItems;
        public int LevelIndex => _levelIndex;
        public int SoftCurrency => _softCurrency;
        public int HardCurrency => _hardCurrency;

        private int _levelIndex;
        private int _softCurrency = 200;
        private int _hardCurrency = 35;

        private string _levelKey = "Level";
        private string _softCurrencyKey = "SoftCurrency";
        private string _hardCurrencyKey = "HardCurrency";
        private string _defencesUnitsUpgradeKey = "DefencesUnitsUpgradeKey";
        private string _unitHandItemsKey = "UnitHandItemsKey";

        private static DataManager instance;

        private List<DefenceUnitType> _unitHandItems;

        private Dictionary<DefenceUnitType, int> _unitsDictionary;

        private bool _isDataLoaded;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        public void LoadData()
        {
            if (_isDataLoaded)
                return;

            _softCurrency = PlayerPrefs.GetInt(_softCurrencyKey, 100);
            _hardCurrency = PlayerPrefs.GetInt(_hardCurrencyKey, 15);
            _levelIndex = PlayerPrefs.GetInt(_levelKey, 0);

            _unitsDictionary = Load<Dictionary<DefenceUnitType, int>>(_defencesUnitsUpgradeKey);
            _unitHandItems = Load<List<DefenceUnitType>>(_unitHandItemsKey);


            _unitHandItems = new List<DefenceUnitType>();

            if (_unitsDictionary.Count == 0)
            {
                Debug.LogError("LoadData ");
                _unitsDictionary = new Dictionary<DefenceUnitType, int>();
                _unitsDictionary.Add(DefenceUnitType.Mining_Unit, 0);
                _unitsDictionary.Add(DefenceUnitType.Shooter_Unit, 0);
                _unitsDictionary.Add(DefenceUnitType.Mine_Unit, 0);
            }

            _isDataLoaded = true;
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
                    PlayerPrefs.SetInt(_softCurrencyKey, _softCurrency);

                    break;
                case CurrencyType.HardCurrency:

                    _hardCurrency -= currencyAmount;
                    PlayerPrefs.SetInt(_hardCurrencyKey, _hardCurrency);
                    break;
            }
        }
        public void UpdateLevel()
        {
            _levelIndex++;
            PlayerPrefs.SetInt(_levelKey, _levelIndex);
        }

        public void AddCurrency(int currencyAmount, CurrencyType currencyType)
        {
            switch (currencyType)
            {
                case CurrencyType.SoftCurrency:
                    _softCurrency += currencyAmount;
                    PlayerPrefs.SetInt(_softCurrencyKey, _softCurrency);
                    break;
                case CurrencyType.HardCurrency:
                    _hardCurrency += currencyAmount;
                    PlayerPrefs.SetInt(_hardCurrencyKey, _hardCurrency);
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
            Debug.LogError("HAND LIST - " + _unitHandItems.Count);

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
            Debug.LogError(key + " ---- " + jsonDataString);
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
