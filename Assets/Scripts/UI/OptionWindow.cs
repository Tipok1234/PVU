using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace Assets.Scripts.UIManager
{
    public class OptionWindow : MonoBehaviour
    {
        //[SerializeField] private Button _soundButton;
        [SerializeField] private Toggle _languageToggle;
        [SerializeField] private Toggle _musicToggle;
        [SerializeField] private Toggle _soundToggle;

        [SerializeField] private TMP_Text _soundText;
        [SerializeField] private TMP_Text _musicText;
        [SerializeField] private TMP_Text _languegeText;

        private void Awake()
        {
            _languageToggle.onValueChanged.AddListener(LanguageToggle);
            _musicToggle.onValueChanged.AddListener(TurnMusic);
            _soundToggle.onValueChanged.AddListener(TurnSound);
        }

        private void LanguageToggle(bool state)
        {         
            if(_languageToggle.isOn)
            {
                _languegeText.text = "EN";
            }
            else
            {
                _languegeText.text = "RUS";
            }
        }

        private void TurnMusic(bool state)
        {
            if (_musicToggle.isOn)
            {
                _musicText.text = "ON";
            }
            else
            {
                _musicText.text = "OFF";
            }
        }

        private void TurnSound(bool state)
        {
            if (_soundToggle.isOn)
            {
                _soundText.text = "ON";
            }
            else
            {
                _soundText.text = "OFF";
            }
        }
        private void Sound()
        {
            AudioListener.pause = !AudioListener.pause;
        }
    }
}
