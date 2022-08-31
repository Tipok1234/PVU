using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UIManager
{
    public class GameOverWindow : MonoBehaviour
    {
        public Canvas RestartGameCanvas => _restartGameCanvas;

        [SerializeField] private Canvas _restartGameCanvas;
        [SerializeField] private Button _restartGameButton;
        [SerializeField] private Button _mainMenuButton;

        void Start()
        {
            _restartGameButton.onClick.AddListener(RestartGame);
            _mainMenuButton.onClick.AddListener(MainMenuScene);
        }

        public void RestartGame()
        {
            _restartGameCanvas.enabled = !_restartGameCanvas.enabled;
        }

        public void RestartGameUI()
        {
            //Debug.LogError("!!!!!!");
            _restartGameCanvas.enabled = true;
        }
        public void MainMenuScene()
        {
            SceneManager.LoadScene("MainMenu");
            _restartGameCanvas.enabled = !_restartGameButton.enabled;
        }
    }
}
