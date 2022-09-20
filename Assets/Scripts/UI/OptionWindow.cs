using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Assets.Scripts.Managers;

namespace Assets.Scripts.UIManager
{
    public class OptionWindow : MonoBehaviour
    {
        [SerializeField] private Toggle _languageToggle;
        [SerializeField] private Toggle _musicToggle;
        [SerializeField] private Toggle _soundToggle;

        [SerializeField] private TMP_Text _soundText;
        [SerializeField] private TMP_Text _musicText;
        [SerializeField] private TMP_Text _languegeText;

        private string _langueageToggleKey = "LanguageKey";
        private string _musicToggleKey = "MusicKey";
        private string _soundToggleKey = "SoundKey";

        private int value;
        private bool _isText;
        private bool _isSound;

        private void Start()
        {
            _languageToggle.onValueChanged.AddListener(LanguageToggle);
            _musicToggle.onValueChanged.AddListener(TurnMusic);
            _soundToggle.onValueChanged.AddListener(TurnSound);


            if (PlayerPrefs.HasKey(_musicToggleKey))
            {
                value = PlayerPrefs.GetInt(_musicToggleKey);

                if (value == 1)
                {
                    _musicToggle.isOn = true;
                    AudioManager.Instance.MainSound.volume = 1;
                    _musicText.text = "ON";
                }
                else
                {
                    _musicToggle.isOn = false;
                    AudioManager.Instance.MainSound.volume = 0;
                    _musicText.text = "OFF";
                }
            }

            if(PlayerPrefs.HasKey(_langueageToggleKey))
            {
                if (_isText)
                {
                    _languageToggle.isOn = true;
                    _languegeText.text = "EN";
                }
                else
                {
                    _languageToggle.isOn = false;
                    _languegeText.text = "RUS";
                }
            }

            if(PlayerPrefs.HasKey(_soundToggleKey))
            {
                if(_isSound)
                {
                    _soundToggle.isOn = true;
                    AudioManager.Instance.TurnOnAllSound();
                    _soundText.text = "ON";
                }
                else
                {
                    _soundToggle.isOn = false;
                    AudioManager.Instance.TurnOffAllSound();
                    _soundText.text = "OFF";
                }
            }
        }

        private void LanguageToggle(bool state)
        {
            AudioManager.Instance.ClickSound();

            if(_languageToggle.isOn)
            {
                _isText = true;
                _languegeText.text = "EN";
              //  PlayerPrefs.SetString(_langueageToggleKey, _languegeText.text);
            }
            else
            {
                _isText = false;
                _languegeText.text = "RUS";
              //  PlayerPrefs.SetString(_langueageToggleKey, _languegeText.text);
            }
          //  PlayerPrefs.Save();
        }

        private void TurnMusic(bool state)
        {
            AudioManager.Instance.ClickSound();

            if (_musicToggle.isOn == true)
            {
                AudioManager.Instance.MainSound.volume = 1;
                PlayerPrefs.SetInt(_musicToggleKey, 1);
                _musicText.text = "ON";
            }
            else
            {
                AudioManager.Instance.MainSound.volume = 0;
                PlayerPrefs.SetInt(_musicToggleKey, 0);
                _musicText.text = "OFF";
            }
            PlayerPrefs.Save();
        }

        private void TurnSound(bool state)
        {
            AudioManager.Instance.ClickSound();

            if (_soundToggle.isOn == true)
            {
                _isSound = true;
                AudioManager.Instance.TurnOnAllSound();
                PlayerPrefs.SetInt(_soundToggleKey, 1);
                _soundText.text = "ON";
            }
            else
            {
                _isSound = false;
                _soundText.text = "OFF";
                AudioManager.Instance.TurnOffAllSound();
                PlayerPrefs.SetInt(_soundToggleKey, 0);
            }
            PlayerPrefs.Save();
        }

        
        //private void Sound()
        //{
        //    AudioListener.pause = !AudioListener.pause;
        //}
    }
}
