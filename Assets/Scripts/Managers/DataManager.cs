using UnityEngine;
using Assets.Scripts.Enums;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Assets.Scripts.Managers
{
    public class DataManager : MonoBehaviour
    {
        public int LevelIndex => _levelIndex;
        public int SoftCurrency => _softCurrency;
        public int HardCurrency => _hardCurrency;

        private int _levelIndex;
        private int _softCurrency = 200;
        private int _hardCurrency = 35;

        private string _levelKey = "Level";
        private string _softCurrencyKey = "SoftCurrency";
        private string _hardCurrencyKey = "HardCurrency";

        private static DataManager instance;


        private Dictionary<DefenceUnitType, int> _unitsDictionary = new Dictionary<DefenceUnitType, int>();
        

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
            _softCurrency = PlayerPrefs.GetInt(_softCurrencyKey, 10);
            // _hardCurrency = PlayerPrefs.GetInt(_hardCurrencyKey, 25);
            _levelIndex = PlayerPrefs.GetInt(_levelKey, 0);
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
                    //_softCurrency = PlayerPrefs.GetInt(_softCurrencyKey, 10);
                    _softCurrency += currencyAmount;
                    PlayerPrefs.SetInt(_softCurrencyKey, _softCurrency);
                    break;
                case CurrencyType.HardCurrency:
                    //_hardCurrency = PlayerPrefs.GetInt(_hardCurrencyKey, 10);
                    _hardCurrency += currencyAmount;
                    PlayerPrefs.SetInt(_hardCurrencyKey, _hardCurrency);
                    break;
            }

        }

        public void BuyUnit(DefenceUnitType defenceUnitType)
        {
            var unitDictionary = _unitsDictionary.ContainsKey(defenceUnitType);

            if (unitDictionary == true)
                return;

            _unitsDictionary.Add(defenceUnitType, 0);
            //Debug.LogError(JsonConvert.SerializeObject(_unitsDictionary));
        }

        public void LevelUpUnit(DefenceUnitType defenceUnitType)
        {
            var unitDictionary = _unitsDictionary.ContainsKey(defenceUnitType);

            if (unitDictionary == false)
                return;

            int level = _unitsDictionary[defenceUnitType];
            level++;
            _unitsDictionary[defenceUnitType] = level;

            // Debug.LogError(JsonConvert.SerializeObject(_unitsDictionary));
        }


        public void Save<T>(string key, T saveData)
        {
            string jsonDataString = JsonUtility.ToJson(saveData, true);
            PlayerPrefs.SetString(key, jsonDataString);
        }
        public T Load<T>(string key) where T : new()
        {
            if (PlayerPrefs.HasKey(key))
            {
                string loadedString = PlayerPrefs.GetString(key);
                return JsonUtility.FromJson<T>(loadedString);
            }
            else
            {
                return new T();
            }
        }
    }
}
