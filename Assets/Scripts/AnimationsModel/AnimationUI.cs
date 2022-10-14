using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts.AnimationsModel
{
    public class AnimationUI : AnimationModel
    {
        private float _fadeTime = 1f;

        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Transform[] _softCurrencyTransoform;

        private Sequence _sequence;
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
            yield return new WaitForSeconds(0.1f);

            for (int i = 0; i < _softCurrencyTransoform.Length; i++)
            {
                _sequence.Append(_softCurrencyTransoform[i].DOMove(new Vector3(2.68f, 5.85f, 0f), 0.5f));
                yield return new WaitForSeconds(0.2f);
            }
        }
    }
}
