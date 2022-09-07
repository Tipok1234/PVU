using UnityEngine;
using Assets.Scripts.Enums;
using System;

namespace Assets.Scripts.Managers
{
    public class DataManager : MonoBehaviour
    {
        public int LevelIndex => _levelIndex;
        public int SoftCurrency => _softCurrency;
        public int HardCurrency => _hardCurrency;

        private int _levelIndex;
        private int _softCurrency;
        private int _hardCurrency;

        private string _levelKey = "Level";
        private string _softCurrencyKey = "SoftCurrency";
        private string _hardCurrencyKey = "HardCurrency";

        private static DataManager instance;

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
            _levelIndex = PlayerPrefs.GetInt(_levelKey, 0);
        }
        public void UpdateLevel()
        {
            _levelIndex++;
            PlayerPrefs.SetInt(_levelKey, _levelIndex);
        }
        public void AddCurrency(int currencyAmount,CurrencyType currencyType)
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
        public void Save<T>(string key, T saveData)
        {
            string jsonDataString = JsonUtility.ToJson(saveData, true);
            PlayerPrefs.SetString(key, jsonDataString);
        }
        public T Load<T>(string key)where T : new()
        {
            if(PlayerPrefs.HasKey(key))
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
