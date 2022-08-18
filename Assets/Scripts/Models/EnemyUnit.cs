using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class EnemyUnit : BaseUnit
    {
        [SerializeField] private Transform _selfTransform;
       // [SerializeField] private Vector3 _walkPoint;

        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _damage;

        private bool _isWalk;
        private bool _isAttack;


        private void FixedUpdate()
        {
            _selfTransform.transform.position += Vector3.forward * _moveSpeed * Time.deltaTime;
        }
    }
}
