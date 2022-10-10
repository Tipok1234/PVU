using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Enums;
using UnityEngine.SceneManagement;
using Assets.Scripts.Managers;

namespace Assets.Scripts.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _optionButton;
        [SerializeField] private Button _shopButton;
        [SerializeField] private Button _openCalendarButton;
        [SerializeField] private Button _handButton;

        [SerializeField] private Canvas _loadingScene;
        [SerializeField] private Canvas _mainCanvas;


        [SerializeField] private BaseWindow[] _baseWindows;

        private Dictionary<WindowType, BaseWindow> _baseWindowsDict = new Dictionary<WindowType, BaseWindow>();

        private void Awake()
        {
            for (int i = 0; i < _baseWindows.Length; i++)
            {
                _baseWindowsDict.Add(_baseWindows[i].WindowType, _baseWindows[i]);
            }
        }

        private void Start()
        {
            for (int i = 0; i < _baseWindows.Length; i++)
            {
                _baseWindows[i].CloseWindowAction += OnOpenWindow;
                _baseWindows[i].CloseWindowAction += OnCloseWindow;
            }

            _startGameButton.onClick.AddListener(StartGame);
            _optionButton.onClick.AddListener(OpenOptionsWindow);
            _handButton.onClick.AddListener(OpenHandWindow);
            _shopButton.onClick.AddListener(OpenShopWindow);
            _openCalendarButton.onClick.AddListener(OpenCalendarWindow);
        }

        private void OnDestroy()
        {
            for (int i = 0; i < _baseWindows.Length; i++)
            {
                _baseWindows[i].CloseWindowAction -= OnOpenWindow;
                _baseWindows[i].CloseWindowAction -= OnCloseWindow;
            }
        }

        private void OnOpenWindow(BaseWindow baseWindow)
        {
           // _mainCanvas.enabled = false;
        }
        private void OnCloseWindow(BaseWindow baseWindow)
        {
           // _mainCanvas.enabled = true;
        }

        private void OpenCalendarWindow()
        {
            OpenWindowByType(WindowType.Calendar);
        }

        private void StartGame()
        {
            if (DataManager.Instance.UnitHandItems.Count == 0)
            {
                OpenHandWindow();
            }
            else
            {
                SceneManager.LoadScene("GameScene");
                _mainCanvas.enabled = !_mainCanvas.enabled;
                _loadingScene.enabled = true;
            }
        }

        private void OpenShopWindow()
        {
            OpenWindowByType(WindowType.Shop);
        }

        private void OpenHandWindow()
        {
            OpenWindowByType(WindowType.Hand);
        }

        private void OpenOptionsWindow()
        {
            OpenWindowByType(WindowType.Options);
        }

        private void OpenWindowByType(WindowType windowType)
        {
            if (_baseWindowsDict.TryGetValue(windowType, out var window))
            {
                window.OpenWindow();
            }
            else
            {
                Debug.LogError("OpenHandCanvas Key Null");
            }
        }
    }
}
