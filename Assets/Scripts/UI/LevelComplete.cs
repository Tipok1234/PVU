using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.SimpleLocalization;
using TMPro;
using Assets.Scripts.AnimationsModel;
using System.Collections;
using DG.Tweening;

namespace Assets.Scripts.UI
{
    public class LevelComplete : MonoBehaviour
    {
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Canvas _nextGameCanvas;
        [SerializeField] private Canvas _loadingCanvas;
        [SerializeField] private TMP_Text _rewardLevelText;
        [SerializeField] private AnimationModel _animationModel;
        [SerializeField] private RectTransform[] _softCurrencyTransoform;
        [SerializeField] private RectTransform _targetTransform;

        private void Awake()
        {
            _nextLevelButton.onClick.AddListener(NextLevelOnClick);
            _mainMenuButton.onClick.AddListener(MainMenuOnClick);
        }

        public void ShowWindow()
        {
            _rewardLevelText.text = LocalizationManager.Localize("GameMenu.LevelReward", 15);
            _nextGameCanvas.enabled = !_nextGameCanvas.enabled;
            _animationModel.PlayAnimation(AnimationCallback);
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

        private void AnimationCallback()
        {
            StartCoroutine(MoveCurrency());
        }    

        private IEnumerator MoveCurrency()
        {
            for (int i = 0; i < _softCurrencyTransoform.Length; i++)
            {
                _softCurrencyTransoform[i].gameObject.SetActive(true);
                _softCurrencyTransoform[i].DOMove(_targetTransform.position, 0.3f).OnComplete(() => { CurrencyCallback(i); });
                yield return new WaitForSeconds(0.15f);
            }
        }

        private void CurrencyCallback(int index)
        {
            _softCurrencyTransoform[index].gameObject.SetActive(false);
        }
    }
}
