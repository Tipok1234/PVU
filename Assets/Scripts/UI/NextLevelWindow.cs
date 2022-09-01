using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextLevelWindow : MonoBehaviour
{
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Canvas _nextGameCanvas;
    [SerializeField] private Canvas _gameCanvas;

    private void Awake()
    {
        _nextLevelButton.onClick.AddListener(NextLevel);
    }

    public void NextLevel()
    {
        _nextGameCanvas.enabled = !_nextGameCanvas.enabled;
        _gameCanvas.enabled = true;
    }    
}
