using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts.AnimationsModel
{
    public class AnimationCart : MonoBehaviour
    {
        [SerializeField] private Transform _cartModel;
        [SerializeField] private float _xStartPos;
        [SerializeField] private float _animationTime;
        public void AnimationsCart()
        {
            _cartModel.DOMoveX(_xStartPos, _animationTime).OnComplete(CallBack);
        }

        private void CallBack()
        {
            Destroy(gameObject);
        }
    }
}

