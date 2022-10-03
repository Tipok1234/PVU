using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.AnimationsModel;
namespace Assets.Scripts.Models
{
    public class Ship : MonoBehaviour
    {
        [SerializeField] private AnimationModel _animationModel;
        private void Awake()
        {
            _animationModel.PlayAnimation();
        }
    }
}
