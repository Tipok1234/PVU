using UnityEngine;
using Assets.Scripts.Enums;
using Assets.Scripts.AnimationsModel;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Models
{
    public class Cart : MonoBehaviour
    {
        [SerializeField] private AnimationModel _animationModel;
        [SerializeField] private ParticleType _particleType;

        [SerializeField] private LayerMask _enemyLayer;

        private bool _isActive = false;

        private Transform _particleTransform;

        private void FixedUpdate()
        {
            var ray = new Ray(transform.position, transform.forward * 0.5f);

            if (Physics.Raycast(ray, out RaycastHit hit, 0.45f, _enemyLayer))
            {
                if (hit.transform.TryGetComponent<AttackUnit>(out AttackUnit enemy))
                {
                    enemy.DeathUnit();

                    if (_isActive)
                        return;

                    _particleTransform = PoolManager.Instance.GetParticleByType(_particleType, transform);

                    _particleTransform.SetParent(transform);

                    _particleTransform.position = gameObject.transform.position;

                    _animationModel.PlayAnimation(AnimationCallback);

                    _isActive = true;
                }
            }
        }

        private void AnimationCallback()
        {
            if (_particleTransform != null)
                PoolManager.Instance.ReturnToPool(_particleTransform);

            gameObject.SetActive(false);
            _isActive = false;
        }
    }
}