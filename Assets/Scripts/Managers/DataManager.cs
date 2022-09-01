using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class DataManager : MonoBehaviour
    {
        private int _levelIndex;
        private int _softCurrency;

        private string _nameLevelKey;
        private string _nameCurrencyKey;

        private void Awake()
        {
            //DontDestroyOnLoad()
        }
        public void UpdateLevel()
        {
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
