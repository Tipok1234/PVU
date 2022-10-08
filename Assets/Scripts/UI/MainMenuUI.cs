using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Managers;
using Assets.Scripts.Enums;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UIManager
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _goFightButton;
        [SerializeField] private Button _optionButton;
        [SerializeField] private Button _shopButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _exitOptionButton;
        [SerializeField] private Button _handButton;
        [SerializeField] private Button _exitHandButton;

        [SerializeField] private Canvas _shopCanvas;
        [SerializeField] private Canvas _loadingScene;
        [SerializeField] private Canvas _mainCanvas;
        [SerializeField] private Canvas _handCanvas;
        [SerializeField] private Canvas _optionCanvas;

        void Start()
        {
            _startGameButton.onClick.AddListener(StartGame);
            _goFightButton.onClick.AddListener(FightGame);
            _optionButton.onClick.AddListener(OpenOptionCanvas);
            _handButton.onClick.AddListener(OpenHandCanvas);
            _exitOptionButton.onClick.AddListener(OpenOptionCanvas);
            _shopButton.onClick.AddListener(OpenShopCanvas);
            _exitButton.onClick.AddListener(OpenShopCanvas);
            _exitHandButton.onClick.AddListener(ExitHandCanvas);
        }

        private void StartGame()
        {
            _handCanvas.enabled = !_handCanvas.enabled;
        }
        private void ExitHandCanvas()
        {
            AudioManager.Instance.PlaySoundGame(AudioSoundType.OpenWindowSound);
            _handCanvas.enabled = !_handCanvas.enabled;
            _mainCanvas.enabled = true;
        }
        private void FightGame()
        {
            SceneManager.LoadScene("GameScene");
            AudioManager.Instance.PlaySoundGame(AudioSoundType.OpenWindowSound);
            _mainCanvas.enabled = !_mainCanvas.enabled;
            _loadingScene.enabled = true;
          //  AudioManager.Instance.OpenWindowSound();
        }
        private void OpenShopCanvas()
        {
            AudioManager.Instance.PlaySoundGame(AudioSoundType.OpenWindowSound);
            //  AudioManager.Instance.OpenWindowSound();
            _shopCanvas.enabled = !_shopCanvas.enabled;
            _mainCanvas.enabled = !_mainCanvas.enabled;
        }

        private void OpenHandCanvas()
        {
            AudioManager.Instance.PlaySoundGame(AudioSoundType.OpenWindowSound);
            //  AudioManager.Instance.OpenWindowSound();
            _handCanvas.enabled = !_handCanvas.enabled;
            _mainCanvas.enabled = !_mainCanvas.enabled;
        }
        private void OpenOptionCanvas()
        {
            AudioManager.Instance.PlaySoundGame(AudioSoundType.OpenWindowSound);
            // AudioManager.Instance.OpenWindowSound();
            _optionCanvas.enabled = !_optionCanvas.enabled;
        }
    }
}
