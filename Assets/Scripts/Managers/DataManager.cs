using UnityEngine;
using System;

namespace Assets.Scripts.Managers
{
    public class DataManager : MonoBehaviour
    {
        public int LevelIndex => _levelIndex;
        public int SoftCurrency => _softCurrency;

        private int _levelIndex;
        private int _softCurrency;

        private string _nameLevelKey;
        private string _nameCurrencyKey;

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
            _softCurrency = PlayerPrefs.GetInt(_nameCurrencyKey, 100);
            _levelIndex = PlayerPrefs.GetInt(_nameLevelKey, 0);
        }
        public void UpdateLevel()
        {
            _levelIndex++;
            PlayerPrefs.SetInt(_nameLevelKey, _levelIndex);
        }
        public void UpdateCurrency()
        {
            PlayerPrefs.SetInt(_nameCurrencyKey,_softCurrency);
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
