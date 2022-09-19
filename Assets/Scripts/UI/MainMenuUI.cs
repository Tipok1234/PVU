using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UIManager
{
    public class MainMenuUI : MonoBehaviour
    {

        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _optionButton;
        [SerializeField] private Button _shopButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _handButton;

        [SerializeField] private Canvas _shopCanvas;
        [SerializeField] private Canvas _loadingScene;
        [SerializeField] private Canvas _gameCanvas;
        [SerializeField] private Canvas _handCanvas;
        void Start()
        {
            _startGameButton.onClick.AddListener(StartGame);
            _optionButton.onClick.AddListener(OpenOptionCanvas);
            _shopButton.onClick.AddListener(OpenShopCanvas);
            _handButton.onClick.AddListener(OpenHandCanvas);
            _exitButton.onClick.AddListener(OpenShopCanvas);
        }

        private void StartGame()
        {
            SceneManager.LoadScene("GameScene");
            _gameCanvas.enabled = !_gameCanvas.enabled;
            _loadingScene.enabled = true;
        }

        private void OpenShopCanvas()
        {
            _shopCanvas.enabled = !_shopCanvas.enabled;
        }

        private void OpenHandCanvas()
        {
            _handCanvas.enabled = !_handCanvas.enabled;
        }
        private void OpenOptionCanvas()
        {

        }
    }
}
