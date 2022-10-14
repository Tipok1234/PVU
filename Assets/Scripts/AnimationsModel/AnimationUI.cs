using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts.AnimationsModel
{
    public class AnimationUI : AnimationModel
    {
        private float _fadeTime = 1f;

        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private RectTransform[] _softCurrencyTransoform;
        [SerializeField] private Transform _targetTransform;
        public override void PlayAnimation()
        {
            _canvasGroup.alpha = 0f;
            _rectTransform.transform.localPosition = new Vector3(0f, -1000f, 0f);
            _rectTransform.DOAnchorPos(new Vector2(0f, 0f), _fadeTime, false).SetEase(Ease.InOutQuint);
            _canvasGroup.DOFade(1, _fadeTime);

            StartCoroutine(MoveCurrency());
        }

        private IEnumerator MoveCurrency()
        {
            for (int i = 0; i < _softCurrencyTransoform.Length; i++)
            {
                _softCurrencyTransoform[i].DOAnchorPos(new Vector3(-867.4f,509.1f,0f), 0.3f); //.OnComplete(()=> _softCurrencyTransoform[i].gameObject.SetActive(false));
                yield return new WaitForSeconds(0.4f);
            }
        }
    }
}
