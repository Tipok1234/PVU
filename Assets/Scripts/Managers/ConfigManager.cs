using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Config;

namespace Assets.Scripts.Managers
{
    public class ConfigManager : MonoBehaviour
    {
        public Config.Config Config => _config;
        public static ConfigManager Intsance => _instance;

        [SerializeField] private Config.Config _config;

        private static ConfigManager _instance;
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
    }
}
