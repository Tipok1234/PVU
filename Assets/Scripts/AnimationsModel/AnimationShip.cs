using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts.AnimationsModel
{
    public class AnimationShip : AnimationModel
    {
        [SerializeField] private Transform _shipModel;
        [SerializeField] private Transform _floorModel;
        [SerializeField] private float _animationTime;
        public override void PlayAnimation()
        {
            _shipModel.DOMove(new Vector3(13.68f, -1.2f, 2.41f), _animationTime).SetEase(Ease.Linear).OnComplete(FloorAnimation);
        }
        public void FloorAnimation()
        {
            _floorModel.DOLocalMoveX(-2.75f, 1.5f).SetEase(Ease.OutCubic);
        }
    }
}
