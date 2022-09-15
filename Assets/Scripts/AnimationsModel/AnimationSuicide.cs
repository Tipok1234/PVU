using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


namespace Assets.Scripts.AnimationsModel
{
    public class AnimationSuicide : AnimationModel
    {
        [SerializeField] private Transform _suicideRightModel;
        [SerializeField] private Transform _suicideLeftModel;
        [SerializeField] private float _xStartPos;
        [SerializeField] private float _xEndPos;
        [SerializeField] private float _animationTime;

        public override void PlayAnimation()
        {
            _suicideRightModel.DOLocalMoveX(_xEndPos, _animationTime).SetEase(Ease.InOutQuad);
            _suicideLeftModel.DOLocalMoveX(_xStartPos, _animationTime).SetEase(Ease.InOutQuad);
        }
    }
}
