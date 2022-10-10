using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class LevelComplete : MonoBehaviour
    {
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Canvas _nextGameCanvas;

        private void Awake()
        {
            _nextLevelButton.onClick.AddListener(NextLevelOnClick);
        }

        public void ShowWindow()
        {
            _nextGameCanvas.enabled = !_nextGameCanvas.enabled;
        }
        public void NextLevelOnClick()
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
