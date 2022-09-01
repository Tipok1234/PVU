using UnityEngine;
using Assets.Scripts.AnimationsModel;

namespace Assets.Scripts.Models
{
    public class Cart : MonoBehaviour
    {
        [SerializeField] private AnimationModel _animationModel;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<AttackUnit>(out AttackUnit attackUnit))
            {
                _animationModel.PlayAnimation();
                attackUnit.Death();
            }
        }
    }
}
