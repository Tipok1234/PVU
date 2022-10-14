using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using Assets.Scripts.AnimationsModel;

namespace Assets.Scripts.UI
{
    public class GameOverWindow : MonoBehaviour
    {
        [SerializeField] private Canvas _restartGameCanvas;
        [SerializeField] private Canvas _loadingScene;
        [SerializeField] private Button _restartGameButton;
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private AnimationModel _animationModel;

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
            _animationModel.PlayAnimation();

            StartCoroutine(StopTime());
        }
        public void MainMenuScene()
        {
            SceneManager.LoadScene("MainMenu");
            _restartGameCanvas.enabled = !_restartGameButton.enabled;
            _loadingScene.enabled = true;
        }

        private IEnumerator StopTime()
        {
            yield return new WaitForSeconds(1f);

            Time.timeScale = 0f;
        }
    }
}
