using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Managers;

namespace Assets.Scripts.UI
{
    public class MapWindow : BaseWindow
    {
        [SerializeField] private Transform[] _levelPosition;
        [SerializeField] private LevelUIItem _levelUIPrefab;

        [SerializeField] private MainMenuUI _mainMenu;

        private LevelUIItem _levelUIItem;

        private List<LevelUIItem> _levelUIItems = new List<LevelUIItem>();
        private void Awake()
        {
            _closeWindowButton.onClick.AddListener(CloseWindow);
        }


        public override void OpenWindow()
        {
            base.OpenWindow();

            for (int i = 0; i < _levelPosition.Length; i++)
            {
                _levelUIItem = Instantiate(_levelUIPrefab, _levelPosition[i]);
                _levelUIItem.Setup(i + 1, DataManager.Instance.LevelIndex >= i);
                _levelUIItem.CheckLevel(DataManager.Instance.LevelIndex > i);
                _levelUIItems.Add(_levelUIItem);
                _levelUIItem.SelectedLevelAction += OnSelectedLevel;
            }
        }

        private void OnSelectedLevel()
        {
            _mainMenu.FightGame();
        }
        public override void CloseWindow()
        {
            base.CloseWindow();

            for (int i = 0; i < _levelUIItems.Count; i++)
            {
                if (_levelUIItems.Count == 0)
                    return;

                Destroy(_levelUIItems[i].gameObject);
            }

            _levelUIItems.Clear();
        }
    }
}
