using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts.AnimationsModel
{
    public class AnimationReward : AnimationModel
    {
        [SerializeField] private Transform _rewardModel;
        [SerializeField] private float _animationTime;


        private Sequence _animation;
        public override void PlayAnimation()
        {
            _animation = DOTween.Sequence();
            _animation.Prepend(_rewardModel.DOScale(Vector2.one * 1.1f, _animationTime).SetEase(Ease.InOutCubic));
            _animation.AppendInterval(0.4f);
            _animation.Append(_rewardModel.DOScale(Vector2.one, _animationTime).SetEase(Ease.InOutCubic));
            _animation.SetLoops(-1);
        }
        public override void ResetAnimation()
        {
            _animation?.Kill();
            _rewardModel.localScale = Vector2.one;
        }
    }
}
