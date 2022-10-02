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

        public override void PlayAnimation()
        {
            _canvasGroup.alpha = 0f;
            _rectTransform.transform.localPosition = new Vector3(0f, -1000f, 0f);
            _rectTransform.DOAnchorPos(new Vector2(0f, 0f), _fadeTime, false).SetEase(Ease.InOutQuint);
            _canvasGroup.DOFade(1, _fadeTime);
        }
    }
}
