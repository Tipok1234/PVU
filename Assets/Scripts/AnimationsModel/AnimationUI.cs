using UnityEngine;
using DG.Tweening;
using System;

namespace Assets.Scripts.AnimationsModel
{
    public class AnimationUI : AnimationModel
    {
        private float _fadeTime = 1f;

        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private RectTransform _rectTransform;

        private Sequence _sequence;

        public override void PlayAnimation()
        {
            _canvasGroup.alpha = 0f;
            _rectTransform.localScale = Vector2.zero;

            _sequence = DOTween.Sequence();
            _sequence.Append(_rectTransform.DOScale(Vector2.one, _fadeTime).SetEase(Ease.InOutQuint));
            _sequence.Join(_canvasGroup.DOFade(1, _fadeTime));
        }

        //public override void PlayAnimationIngorTimeScale()
        //{
        //    _canvasGroup.alpha = 0f;
        //    _rectTransform.localScale = Vector2.zero;

        //    _sequence = DOTween.Sequence().SetUpdate(false);
        //    _sequence.Append(_rectTransform.DOScale(Vector2.one, _fadeTime).SetEase(Ease.InOutQuint)).SetUpdate(false);
        //    _sequence.Join(_canvasGroup.DOFade(1, _fadeTime).SetUpdate(false));
        //}

        public override void PlayAnimation(Action callback)
        {
            _canvasGroup.alpha = 0f;
            _rectTransform.localScale = Vector2.zero;

            _sequence = DOTween.Sequence();
            _sequence.Append(_rectTransform.DOScale(Vector2.one, _fadeTime).SetEase(Ease.InOutQuint));
            _sequence.Join(_canvasGroup.DOFade(1, _fadeTime));
            _sequence.AppendCallback(() => { callback?.Invoke(); });
        }

        public override void OnDestroy()
        {
            _sequence?.Kill();
        }
    }
}
