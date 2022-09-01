using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts.AnimationsModel
{
    public class AnimationGun : AnimationModel
    {
        [SerializeField] private Transform _gunModel;
        [SerializeField] private float _xStartPos;
        [SerializeField] private float _xEndPos;
        [SerializeField] private float _animationTime;

        public override void PlayAnimation()
        {
            Sequence mySequence = DOTween.Sequence();
            mySequence.Prepend(_gunModel.DOLocalMoveX(_xEndPos, _animationTime));
            mySequence.AppendInterval(0.4f);
            mySequence.Append(_gunModel.DOLocalMoveX(_xStartPos, _animationTime));
        }
    }
}
