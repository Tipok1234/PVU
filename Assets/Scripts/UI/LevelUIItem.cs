using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace Assets.Scripts.UI
{
    public class LevelUIItem : MonoBehaviour
    {
        public event Action SelectedLevelAction;
        public bool IsLock => _isLock;
        public Sprite LockImage => _lockImage.sprite;

        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Image _lockImage;
        [SerializeField] private Image _passedImage;

        private bool _isLock;
        private bool _isPassed = false;

        private void Awake()
        {
            _startGameButton.onClick.AddListener(StartLevel);
        }

        public void Setup(int indexLevel, bool unlock)
        {
            _levelText.text = indexLevel.ToString();

            if(unlock == true)
            {
                _lockImage.enabled = false;
                _isLock = false;
            }
            else
            {
                _isLock = true;
                _lockImage.enabled = true;
            }
        }

        public void CheckLevel(bool passed)
        {
            if(passed == true)
            {
                _passedImage.enabled = true;
                _isPassed = true;
            }
            else
            {
                _passedImage.enabled = false;
                _isPassed = false;
            }
        }
        private void StartLevel()
        {
            if(_isLock == false)
            {
                if (_passedImage.enabled == true)
                    return;

                SelectedLevelAction?.Invoke();
            }
        }
    }
}
