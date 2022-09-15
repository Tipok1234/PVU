using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts.AnimationsModel
{
    public class AnimationMining : AnimationModel
    {
        [SerializeField] private Transform _miningModel;
        [SerializeField] private float _animationTime;

        public override void PlayAnimation()
        {
            Sequence mySequence = DOTween.Sequence();
            mySequence.Prepend(_miningModel.DOScale(new Vector3(0.5f, 0.5f, 0.5f), _animationTime).SetEase(Ease.InOutCubic));
            mySequence.AppendInterval(0.4f);
            mySequence.Append(_miningModel.DOScale(Vector3.one, _animationTime).SetEase(Ease.InOutCubic));
        }
    }
}
