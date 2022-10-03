using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Managers;
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

        [SerializeField] private Canvas _shopCanvas;
        [SerializeField] private Canvas _loadingScene;
        [SerializeField] private Canvas _gameCanvas;
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

        }

        private void StartGame()
        {
            _handCanvas.enabled = !_handCanvas.enabled;
        }

        private void FightGame()
        {
            SceneManager.LoadScene("GameScene");
            _gameCanvas.enabled = !_gameCanvas.enabled;
            _loadingScene.enabled = true;
            AudioManager.Instance.OpenWindowSound();
        }
        private void OpenShopCanvas()
        {
            AudioManager.Instance.OpenWindowSound();
            _shopCanvas.enabled = !_shopCanvas.enabled;
        }

        private void OpenHandCanvas()
        {
            AudioManager.Instance.OpenWindowSound();
            _handCanvas.enabled = !_handCanvas.enabled;
        }
        private void OpenOptionCanvas()
        {
            AudioManager.Instance.OpenWindowSound();
            _optionCanvas.enabled = !_optionCanvas.enabled;
        }
    }
}
