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

        public override void PlayAnimation()
        {
            _cartModel.DOMoveX(_xStartPos, _animationTime).OnComplete(CallBack);
        }

        public override void PlayAnimation(Action callback)
        {
            //void action()
            //{
            //    callback?.Invoke();
            //    CallBack(); 
            //}

            //_cartModel.DOMoveX(_xStartPos, _animationTime).OnComplete(action);

            _cartModel.DOMoveX(_xStartPos, _animationTime).OnComplete(() =>
            {
                callback?.Invoke();
                CallBack();
            });
        }

        private void CallBack()
        {
            Debug.LogError("C");

            Destroy(gameObject);
        }
    }
}

