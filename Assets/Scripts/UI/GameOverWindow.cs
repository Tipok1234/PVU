using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UIManager
{
    public class GameOverWindow : MonoBehaviour
    {
        public Canvas RestartGameCanvas => _restartGameCanvas;

        [SerializeField] private Canvas _restartGameCanvas;
        [SerializeField] private Canvas _loadingScene;
        [SerializeField] private Button _restartGameButton;
        [SerializeField] private Button _mainMenuButton;

        void Start()
        {
            Time.timeScale = 1f;
            _restartGameButton.onClick.AddListener(RestartGame);
            _mainMenuButton.onClick.AddListener(MainMenuScene);
        }

        public void RestartGame()
        {
            SceneManager.LoadScene("GameScene");
        }

        public void RestartGameUI()
        {
            _restartGameCanvas.enabled = true;
            Time.timeScale = 0f;
        }
        public void MainMenuScene()
        {
            SceneManager.LoadScene("MainMenu");
            _restartGameCanvas.enabled = !_restartGameButton.enabled;
            _loadingScene.enabled = true;
        }
    }
}
