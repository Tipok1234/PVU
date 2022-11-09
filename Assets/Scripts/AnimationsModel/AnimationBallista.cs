using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts.AnimationsModel
{
    public class AnimationBallista : AnimationModel
    {
        [SerializeField] private Transform _gunModel;
        [SerializeField] private float _animationTime;
        [SerializeField] private float _xStartPos;
        [SerializeField] private float _xEndPos;

        public override void PlayAnimation()
        {
            Sequence mySequence = DOTween.Sequence();
            mySequence.Prepend(_gunModel.DOLocalMoveZ(_xEndPos, _animationTime).SetEase(Ease.OutElastic));
            mySequence.AppendInterval(_animationTime);
            mySequence.Append(_gunModel.DOLocalMoveZ(_xStartPos, _animationTime).SetEase(Ease.Linear));
        }
    }
}
