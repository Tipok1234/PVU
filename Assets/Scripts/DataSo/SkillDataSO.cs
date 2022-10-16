using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Enums;

namespace Assets.Scripts.DataSo
{
    [CreateAssetMenu(fileName = "SkillData", menuName = "ScriptableObjects/SkillData", order = 1)]
    public class SkillDataSO : ScriptableObject
    {
        public SkillType SkillType => _skillType;
        public ParticleType ParticleType => _particleType;
        public float DamageSkill => _damageSkill;
        public float CooldownSkill => _cooldownSkill;
        public float PriceSkill => _priceSkill;

        [SerializeField] private Sprite _spriteSkill;
        [SerializeField] private SkillType _skillType;
        [SerializeField] private ParticleType _particleType;
        [SerializeField] private float _damageSkill;
        [SerializeField] private float _cooldownSkill;
        [SerializeField] private float _priceSkill;
    }
}
