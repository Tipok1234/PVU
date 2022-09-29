using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts.AnimationsModel
{
    public class AnimationBomb : AnimationModel
    {
        [SerializeField] private Transform _bombModel;
        [SerializeField] private float _animationTime;
        public override void PlayAnimation()
        {
            _bombModel.DOScale(new Vector3(2f, 2f, 2f), _animationTime).SetEase(Ease.InQuart).OnComplete(ResetAnimation);
        }

        public override void ResetAnimation()
        {
            _bombModel.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
