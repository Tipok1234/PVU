using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

namespace Assets.Scripts.Managers
{
    public class AudioManager : MonoBehaviour
    {
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
    }
}
