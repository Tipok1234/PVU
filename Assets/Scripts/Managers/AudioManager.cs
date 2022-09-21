using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

namespace Assets.Scripts.Managers
{
    public class AudioManager : MonoBehaviour
    {
        //public float Music => _music;
        //public float Sound => _sound;
        public static AudioManager Instance => _instance;
        public AudioSource MainSound => _mainSound;
        public AudioMixer AudioMixer => _audioMixer;
        public AudioMixer AudioMixerSound => _audioMixerSound;

        [SerializeField] private AudioSource _levelUpSound;
        [SerializeField] private AudioSource _noMoneySound;
        [SerializeField] private AudioSource _openWindowSound;
        [SerializeField] private AudioSource _mainSound;
        [SerializeField] private AudioSource _clickSound;

        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private AudioMixer _audioMixerSound;

        private static AudioManager _instance;

        //private string _mixerMusicKey = "MusicMixer";
        //private string _mixerSoundKey = "SoundMixer";

        //private float _music;
        //private float _sound;

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
            //_music = PlayerPrefs.GetFloat(_mixerMusicKey, 0.5f);
            //_sound = PlayerPrefs.GetFloat(_mixerSoundKey, 0.5f);

            //Debug.LogError("MUSIC : " + _music);
            //Debug.LogError("SOUND : " + _sound);

            //SetVolumeMainSound(_music);
            //SetVolumeSound(_sound);
        }

        public void ClickSound()
        {
            _clickSound.Play();
        }
        public void LevelUpSound()
        {
            _levelUpSound.Play();
        }
        public void NoMoneySound()
        {
            _noMoneySound.Play();
        }
        public void OpenWindowSound()
        {
            _openWindowSound.Play();
        }

        public void TurnOffAllSound()
        {
            _levelUpSound.volume = 0;
            _noMoneySound.volume = 0;
            _openWindowSound.volume = 0;
            _clickSound.volume = 0;
        }
        public void TurnOnAllSound()
        {
            _levelUpSound.volume = 1;
            _noMoneySound.volume = 1;
            _openWindowSound.volume = 1;
            _clickSound.volume = 1;
        }

        //public void SetVolumeMainSound(float volume)
        //{
        //    _audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
        //    _audioMixer.SetFloat(_mixerMusicKey,volume);
        //    PlayerPrefs.SetFloat(_mixerMusicKey, volume);
        //    PlayerPrefs.Save();
        //}

        //public void SetVolumeSound(float volume)
        //{
        //    _audioMixerSound.SetFloat("volume", Mathf.Log10(volume) * 20);
        //    _audioMixerSound.SetFloat(_mixerSoundKey, volume);
        //    PlayerPrefs.SetFloat(_mixerSoundKey, volume);
        //    PlayerPrefs.Save();
        //}
    }
}
