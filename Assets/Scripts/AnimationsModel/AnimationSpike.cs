using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts.AnimationsModel
{
    public class AnimationSpike : AnimationModel
    {
        [SerializeField] private Transform _spikeModel;
        [SerializeField] private float _animationTime;
        public override void PlayAnimation()
        {
            //_spikeModel.DOMoveY(0.2f, _animationTime).SetEase(Ease.OutElastic);

            Sequence mySequence = DOTween.Sequence();
            mySequence.Prepend(_spikeModel.DOMoveY(0.07f, _animationTime).SetEase(Ease.InElastic));
            mySequence.AppendInterval(_animationTime);
            mySequence.Append(_spikeModel.DOMoveY(0f, _animationTime).SetEase(Ease.Linear));
        }
    }
}
