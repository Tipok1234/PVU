using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts.AnimationsModel
{
    public class AnimationChest : AnimationModel
    {
        [SerializeField] private Transform _chestModel;
        [SerializeField] private float _animationTime;
        public override void PlayAnimation()
        {
            Sequence mySequence = DOTween.Sequence();
            mySequence.Prepend(_chestModel.DORotate(_chestModel.transform.eulerAngles + new Vector3(-170f, 0f, 0f), _animationTime).SetEase(Ease.InOutCubic));
            mySequence.AppendInterval(_animationTime);
        }
    }
}
