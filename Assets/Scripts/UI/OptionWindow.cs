using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Assets.Scripts.Enums;
using Assets.Scripts.Managers;
using Assets.SimpleLocalization;

namespace Assets.Scripts.UI
{
    public class OptionWindow : BaseWindow
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
        private bool _isMusic;
        

        private void Awake()
        {
            LocalizationManager.LocalizationChanged += OnMusicChanged;
        }

        private void Start()
        {
            _languageToggle.onValueChanged.AddListener(LanguageToggle);
            _musicToggle.onValueChanged.AddListener(TurnMusic);
            _soundToggle.onValueChanged.AddListener(TurnSound);

            _closeWindowButton.onClick.AddListener(CloseWindow);


            if (PlayerPrefs.HasKey(_musicToggleKey))
            {
                value = PlayerPrefs.GetInt(_musicToggleKey);

                if (value == 1)
                {
                    _musicToggle.isOn = true;
                    AudioManager.Instance.TurnOnMainMusic();
                    _musicText.text = LocalizationManager.Localize(LocalizationConst.Options + "On");
                }
                else
                {
                    _musicToggle.isOn = false;
                    AudioManager.Instance.TurnOffMainMusic();
                    _musicText.text = LocalizationManager.Localize(LocalizationConst.Options + "Off");
                }
            }

            if (PlayerPrefs.HasKey(_langueageToggleKey))
            {
                value = PlayerPrefs.GetInt(_langueageToggleKey);

                if (value == 1)
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

            if (PlayerPrefs.HasKey(_soundToggleKey))
            {
                value = PlayerPrefs.GetInt(_soundToggleKey);

                if (value == 1)
                {
                    _soundToggle.isOn = true;
                    AudioManager.Instance.TurnOnAllSound();
                    _soundText.text = LocalizationManager.Localize(LocalizationConst.Options + "On");
                }
                else
                {
                    _soundToggle.isOn = false;
                    AudioManager.Instance.TurnOffAllSound();
                    _soundText.text = LocalizationManager.Localize(LocalizationConst.Options + "Off"); 
                }
            }
        }

        private void LanguageToggle(bool state)
        {
            AudioManager.Instance.PlaySoundGame(AudioSoundType.ClickSound);

            value = _isText ? 1 : 0;

            if (_languageToggle.isOn)
            {
                _languegeText.text = "EN";
                LocalizationManager.SetLanguage(LocalizationManager.LanguageEnum.English);
                PlayerPrefs.SetInt(_langueageToggleKey, 1);
            }
            else
            {
                _languegeText.text = "RUS";
                LocalizationManager.SetLanguage(LocalizationManager.LanguageEnum.Russian);
                PlayerPrefs.SetInt(_langueageToggleKey, 0);
            }
            PlayerPrefs.Save();
        }

        private void TurnMusic(bool state)
        {
            AudioManager.Instance.PlaySoundGame(AudioSoundType.ClickSound);

            value = _isMusic ? 1 : 0;

            if (_musicToggle.isOn == true)
            {
                _musicText.text = LocalizationManager.Localize(LocalizationConst.Options + "On");
                AudioManager.Instance.TurnOnMainMusic();
                PlayerPrefs.SetInt(_musicToggleKey, 1);
            }
            else
            {
                _musicText.text = LocalizationManager.Localize(LocalizationConst.Options + "Off");
                AudioManager.Instance.TurnOffMainMusic();
                PlayerPrefs.SetInt(_musicToggleKey, 0);
            }
            PlayerPrefs.Save();
        }

        private void TurnSound(bool state)
        {
            AudioManager.Instance.PlaySoundGame(AudioSoundType.ClickSound);

            value = _isSound ? 1 : 0;

            if (_soundToggle.isOn == true)
            {
                AudioManager.Instance.TurnOnAllSound();
                PlayerPrefs.SetInt(_soundToggleKey, 1);
                _soundText.text = LocalizationManager.Localize(LocalizationConst.Options + "On");
            }
            else
            {
                _soundText.text = LocalizationManager.Localize(LocalizationConst.Options + "Off");
                AudioManager.Instance.TurnOffAllSound();
                PlayerPrefs.SetInt(_soundToggleKey, 0);
            }
            PlayerPrefs.Save();
        }

        private void OnMusicChanged()
        {
            if (_musicToggle.isOn)
                _musicText.text = LocalizationManager.Localize(LocalizationConst.Options + "On");
            else
                _musicText.text = LocalizationManager.Localize(LocalizationConst.Options + "Off");

            if (_soundToggle.isOn)
                _soundText.text = LocalizationManager.Localize(LocalizationConst.Options + "On");
            else
                _soundText.text = LocalizationManager.Localize(LocalizationConst.Options + "Off");
        }
    }
}
