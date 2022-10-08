using UnityEngine;
using DG.Tweening;
using System;

namespace Assets.Scripts.AnimationsModel
{
    public class AnimationShip : AnimationModel
    {
        [SerializeField] private Transform _shipModel;
        [SerializeField] private Transform _floorModel;
        [SerializeField] private float _animationTime;

        private Sequence _sequence;
        public override void PlayAnimation(Action callback)
        {
            _sequence = DOTween.Sequence();
            _sequence.Append(_shipModel.DOMove(new Vector3(13.68f, -1.2f, 2.41f), _animationTime).SetEase(Ease.Linear));
            _sequence.AppendInterval(0.5f);
            _sequence.Append(_floorModel.DOLocalMoveX(-2.75f, 1.5f).SetEase(Ease.OutCubic));
            _sequence.OnComplete(()=> { callback?.Invoke(); });
        }
    }
}
