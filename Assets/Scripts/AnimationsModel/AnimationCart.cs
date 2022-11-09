using UnityEngine;
using DG.Tweening;
using System;

namespace Assets.Scripts.AnimationsModel
{
    public class AnimationCart : AnimationModel
    {
        [SerializeField] private Transform _cartModel;
        [SerializeField] private float _xStartPos;
        [SerializeField] private float _animationTime;

        public override void PlayAnimation(Action callback)
        {
            _cartModel.DOMoveX(_xStartPos, _animationTime).OnComplete(() =>
            {
                callback?.Invoke();
            });
        }
    }
}

