using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts.AnimationsModel
{
    public class AnimationMining : MonoBehaviour
    {
        [SerializeField] private Transform _miningModel;
        [SerializeField] private float _animationTime;

        public void AnimationsMining()
        {
            Sequence mySequence = DOTween.Sequence();
            mySequence.Prepend(_miningModel.DOScale(new Vector3(5f, 5f, 5f), _animationTime));
            mySequence.AppendInterval(0.4f);
            mySequence.Append(_miningModel.DOScale(new Vector3(10f, 10f, 10f), _animationTime));
        }
    }
}
