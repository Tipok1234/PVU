using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts.AnimationsModel
{
    public class AnimationMine : AnimationModel
    {
        [SerializeField] private Transform _mineModel;
        [SerializeField] private float _animationTime;
        public override void PlayAnimation()
        {
            _mineModel.DOMoveY(0.2f, _animationTime);
        }
    }
}
