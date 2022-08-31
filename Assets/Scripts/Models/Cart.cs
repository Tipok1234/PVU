using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Controller;
using Assets.Scripts.AnimationsModel;

namespace Assets.Scripts.Models
{
    public class Cart : MonoBehaviour
    {
        [SerializeField] private AnimationCart _animationUnit;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<AttackUnit>(out AttackUnit attackUnit))
            {
                _animationUnit.AnimationsCart();
                attackUnit.Death();
            }
        }
    }
}
