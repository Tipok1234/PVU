using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts.Controller
{
    public class AnimationUnit : MonoBehaviour
    {
        [SerializeField] private Transform _gunModel;
        [SerializeField] private float _xStartPos;
        [SerializeField] private float _xEndPos;
       // [SerializeField] private float _xGapPos;
        [SerializeField] private float _animationTime;

        public void AnimationGun()
        {
            Sequence mySequence = DOTween.Sequence();
            mySequence.Prepend(_gunModel.DOLocalMoveX(_xEndPos, _animationTime));
            mySequence.AppendInterval(0.4f);
            mySequence.Append(_gunModel.DOLocalMoveX(_xStartPos, _animationTime));           
        }

        public void AnimationMining()
        {
            Sequence mySequence = DOTween.Sequence();
            mySequence.Prepend(_gunModel.DOScale(new Vector3(5f, 5f, 5f), _animationTime));
            mySequence.AppendInterval(0.4f);
            mySequence.Append(_gunModel.DOScale(new Vector3(10f, 10f, 10f), _animationTime));
        }

        //public void AnimationDoubleGunOne()
        //{
        //    Sequence mySequence = DOTween.Sequence();
        //    mySequence.Prepend(_gunModel.DOLocalMoveX(_xEndPos, _animationTime));
        //    mySequence.AppendInterval(0.1f);
        //    mySequence.Append(_gunModel.DOLocalMoveX(_xStartPos, _animationTime));

        //}
        //public void AnimationDoubleGuntTwo()
        //{
        //    Sequence mySequence = DOTween.Sequence();
        //    mySequence.Prepend(_gunModel.DOLocalMoveX(_xGapPos, _animationTime));
        //    mySequence.AppendInterval(0.4f);
        //    mySequence.Append(_gunModel.DOLocalMoveX(_xStartPos, _animationTime));
        //}
    }
}
