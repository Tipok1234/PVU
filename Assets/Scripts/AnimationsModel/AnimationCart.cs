using UnityEngine;
using DG.Tweening;

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
        private void CallBack()
        {
            Destroy(gameObject);
        }
    }
}

