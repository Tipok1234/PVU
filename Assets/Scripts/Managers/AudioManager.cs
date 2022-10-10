using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Managers
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance => _instance;

        [SerializeField] private AudioSource[] _allSoundsGames;

        [SerializeField] private AudioSource _mainSound;

        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private AudioMixer _audioMixerSound;

        [SerializeField] private AudioSoundType[] _audioType;

        [SerializeField] private Slider _sliderMusic;
        [SerializeField] private Slider _sliderSound;

        private static AudioManager _instance;

        private string _mixerMusicKey = "MusicMixer";
        private string _mixerSoundKey = "SoundMixer";

        private float _music;
        private float _sound;

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
            _mainSound.Play();
        }

        private void Start()
        {
            _music = PlayerPrefs.GetFloat(_mixerMusicKey, 0.5f);
            _sound = PlayerPrefs.GetFloat(_mixerSoundKey, 0.5f);

            SetSliderValue();

            SetVolumeMainSound(_music);
            SetVolumeSound(_sound);
        }

        public void PlaySoundGame(AudioSoundType audioType)
        {
            for (int i = 0; i < _allSoundsGames.Length; i++)
            {
                if (_audioType[i] == audioType)
                {
                    _allSoundsGames[i].Play();
                }          
            }
        }

        public void TurnOnMainMusic()
        {
            _mainSound.volume = 1;
        }
        public void TurnOffMainMusic()
        {
            _mainSound.volume = 0;
        }
        public void TurnOffAllSound()
        {
            for (int i = 0; i < _allSoundsGames.Length; i++)
            {
                _allSoundsGames[i].volume = 0;
            }
        }
        public void TurnOnAllSound()
        {
            for (int i = 0; i < _allSoundsGames.Length; i++)
            {
                _allSoundsGames[i].volume = 1;
            }
        }

        public void SetVolumeMainSound(float volume)
        {
            _audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
            _audioMixer.SetFloat(_mixerMusicKey, volume);
            PlayerPrefs.SetFloat(_mixerMusicKey, volume);
            PlayerPrefs.Save();
        }

        public void SetVolumeSound(float volume)
        {
            _audioMixerSound.SetFloat("volume", Mathf.Log10(volume) * 20);
            _audioMixerSound.SetFloat(_mixerSoundKey, volume);
            PlayerPrefs.SetFloat(_mixerSoundKey, volume);
            PlayerPrefs.Save();
        }

        public void SetSliderValue()
        {
            _sliderMusic.value = _music;
            _sliderSound.value = _sound;
        }
    }
}
