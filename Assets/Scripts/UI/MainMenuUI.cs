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
        [SerializeField] private Canvas _optionCanvas;
        [SerializeField] private Canvas _gameCanvas;
        void Start()
        {
            _startGameButton.onClick.AddListener(StartGame);
            _optionButton.onClick.AddListener(OpenOptionCanvas);
        }

        private void StartGame()
        {
            SceneManager.LoadScene("SampleScene");
            _gameCanvas.enabled = !_gameCanvas.enabled;
            _optionCanvas.enabled = true;
        }

        private void OpenOptionCanvas()
        {

        }
    }
}
