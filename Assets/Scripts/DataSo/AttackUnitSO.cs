using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Enums;

namespace Assets.Scripts.DataSo
{
    [CreateAssetMenu(fileName = "AttackUnit", menuName = "ScriptableObjects/AttackUnit", order = 3)]
    public class AttackUnitSO : ScriptableObject
    {
        public AttackUnitType AttackUnitType => _attackUnitType;
        public float Damage => _damage;
        public float MoveSpeed => _moveSpeed;
        public float ReloadTimeAttack => _reloadTimeAttack;
        public float HP => _hp;

        [SerializeField] private AttackUnitType _attackUnitType;
        [SerializeField] private float _damage;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _reloadTimeAttack;
        [SerializeField] private float _hp;
    }
}
