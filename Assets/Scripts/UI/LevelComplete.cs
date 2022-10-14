using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.SimpleLocalization;
using TMPro;

namespace Assets.Scripts.UI
{
    public class LevelComplete : MonoBehaviour
    {
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Canvas _nextGameCanvas;
        [SerializeField] private Canvas _loadingCanvas;
        [SerializeField] private TMP_Text _rewardLevelText;

        private void Awake()
        {
            _nextLevelButton.onClick.AddListener(NextLevelOnClick);
            _mainMenuButton.onClick.AddListener(MainMenuOnClick);
        }

        public void ShowWindow()
        {
            _rewardLevelText.text = LocalizationManager.Localize("GameMenu.LevelReward", 15);
            _nextGameCanvas.enabled = !_nextGameCanvas.enabled;
        }
        public void NextLevelOnClick()
        {
            SceneManager.LoadScene("GameScene");
        }
        public void MainMenuOnClick()
        {
            SceneManager.LoadScene("MainMenu");
            _nextGameCanvas.enabled = !_nextGameCanvas.enabled;
            _loadingCanvas.enabled = true;
        }
    }
}
