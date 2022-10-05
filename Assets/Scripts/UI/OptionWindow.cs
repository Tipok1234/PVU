using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Assets.Scripts.Enums;
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

        [SerializeField] private Slider _sliderMusic;
        [SerializeField] private Slider _sliderSound;

        private string _langueageToggleKey = "LanguageKey";
        private string _musicToggleKey = "MusicKey";
        private string _soundToggleKey = "SoundKey";

        private string _mixerMusicKey = "MusicMixer";        
        private string _mixerSoundKey = "SoundMixer";

        private float _music;                               
        private float _sound;

        private int value;
        private bool _isText;
        private bool _isSound;

        private void Start()
        {
            _languageToggle.onValueChanged.AddListener(LanguageToggle);
            _musicToggle.onValueChanged.AddListener(TurnMusic);
            _soundToggle.onValueChanged.AddListener(TurnSound);


            _music = PlayerPrefs.GetFloat(_mixerMusicKey, 0.5f);
            _sound = PlayerPrefs.GetFloat(_mixerSoundKey, 0.5f);

            GetSliderValue();

            SetVolumeMainSound(_music);
            SetVolumeSound(_sound);


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

            if(PlayerPrefs.HasKey(_soundToggleKey))
            {
                value = PlayerPrefs.GetInt(_soundToggleKey);

                if (value == 1)
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
            AudioManager.Instance.PlaySoundGame(AudioSoundType.ClickSound);

            value = _isText ? 1 : 0;

            if(_languageToggle.isOn)
            {
                _languegeText.text = "EN";
                PlayerPrefs.SetInt(_langueageToggleKey, 1);
            }
            else
            {
                _languegeText.text = "RUS";
                PlayerPrefs.SetInt(_langueageToggleKey, 0);
            }
            PlayerPrefs.Save();
        }

        private void TurnMusic(bool state)
        {
            AudioManager.Instance.PlaySoundGame(AudioSoundType.ClickSound);

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
            AudioManager.Instance.PlaySoundGame(AudioSoundType.ClickSound);

            value = _isSound ? 1 : 0;

            if (_soundToggle.isOn == true)
            {
                AudioManager.Instance.TurnOnAllSound();
                PlayerPrefs.SetInt(_soundToggleKey, 1);
                _soundText.text = "ON";
            }
            else
            {
                _soundText.text = "OFF";
                AudioManager.Instance.TurnOffAllSound();
                PlayerPrefs.SetInt(_soundToggleKey, 0);
            }
            PlayerPrefs.Save();
        }

        public void GetSliderValue()
        {
            _sliderMusic.value = _music;
            _sliderSound.value = _sound;
        }

        public void SetVolumeMainSound(float volume)
        {
            AudioManager.Instance.AudioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
            AudioManager.Instance.AudioMixer.SetFloat(_mixerMusicKey, volume);
            PlayerPrefs.SetFloat(_mixerMusicKey, volume);
            PlayerPrefs.Save();
        }

        public void SetVolumeSound(float volume)
        {
            AudioManager.Instance.AudioMixerSound.SetFloat("volume", Mathf.Log10(volume) * 20);
            AudioManager.Instance.AudioMixerSound.SetFloat(_mixerSoundKey, volume);
            PlayerPrefs.SetFloat(_mixerSoundKey, volume);
            PlayerPrefs.Save();
        }
    }
}
