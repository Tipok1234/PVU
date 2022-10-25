using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Managers;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI
{
    public class MapController : BaseWindow
    {
        [SerializeField] private Transform[] _levelPosition;
        [SerializeField] private LevelUIItem _levelUIPrefab;

        [SerializeField] private MainMenuUI _mainMenu;

       // private List<LevelUIItem> _levelUIItems = new List<LevelUIItem>();
        private void Awake()
        {
            _closeWindowButton.onClick.AddListener(CloseWindow);
        }


        public override void OpenWindow()
        {
            base.OpenWindow();

            for (int i = 0; i < _levelPosition.Length; i++)
            {
                LevelUIItem levelItem = Instantiate(_levelUIPrefab, _levelPosition[i]);
                levelItem.Setup(i + 1, DataManager.Instance.LevelIndex >= i);
                levelItem.CheckLevel(DataManager.Instance.LevelIndex > i);
              //_levelUIItems.Add(levelItem);
                levelItem.SelectedLevelAction += OnSelectedLevel;
            }
        }

        private void OnSelectedLevel()
        {
            _mainMenu.FightGame();
            CloseWindow();
        }
    }
}
