using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Managers
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance => _instance;
        public AudioSource MainSound => _mainSound;
        public AudioMixer AudioMixer => _audioMixer;
        public AudioMixer AudioMixerSound => _audioMixerSound;

        [SerializeField] private AudioSource[] _allSoundsGames;

        [SerializeField] private AudioSource _mainSound;

        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private AudioMixer _audioMixerSound;

        [SerializeField] private AudioSoundType[] _audioType;

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

        public void PlaySoundGame(AudioSoundType audioType)
        {
            for (int i = 0; i < _allSoundsGames.Length; i++)
            {
                if (_audioType[i] == audioType)
                {
                    Debug.LogError(_allSoundsGames[i].name + "AUDIO");
                    _allSoundsGames[i].Play();
                }          
            }
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
    }
}
