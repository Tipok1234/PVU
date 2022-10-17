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
        [SerializeField] private Image[] _softCurrencyTransoform;
        [SerializeField] private Transform _targetTransform;

        private void Awake()
        {
            _nextLevelButton.onClick.AddListener(NextLevelOnClick);
            _mainMenuButton.onClick.AddListener(MainMenuOnClick);
        }

        public void ShowWindow()
        {
            _rewardLevelText.text = LocalizationManager.Localize(LocalizationConst.GameMenu + "LevelReward", 15);
            _nextGameCanvas.enabled = !_nextGameCanvas.enabled;
            _animationModel.PlayAnimation(AnimationCallback);
        }
        private void NextLevelOnClick()
        {
            SceneManager.LoadScene("GameScene");
        }
        private void MainMenuOnClick()
        {
            _nextGameCanvas.enabled = !_nextGameCanvas.enabled;
            _loadingCanvas.enabled = true;
            SceneManager.LoadScene("MainMenu");
        }

        private void AnimationCallback()
        {
            StartCoroutine(MoveCurrency());
        }    

        private IEnumerator MoveCurrency()
        {
            for (int i = 0; i < _softCurrencyTransoform.Length; i++)
            {
                int index = i;
                _softCurrencyTransoform[i].enabled = true;
                _softCurrencyTransoform[i].transform.DOMove(_targetTransform.position, 0.3f).OnComplete(() => { _softCurrencyTransoform[index].enabled = false; });
                yield return new WaitForSeconds(0.15f);
            }
        }
    }
}
