using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.UI;
using Assets.Scripts.Managers;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Models
{
    public class RainSkill : BaseSkill
    {
        [SerializeField] private SkillGameUI _skillGameUI;
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private Transform _skillPos;
        public override void UseSkill()
        {
            _skillGameUI.RechargeTimeSkill();
            var rayCast = Physics.OverlapSphere(new Vector3(0, 0, 0), 14f, _enemyLayer);

            for (int i = 0; i < rayCast.Length; i++)
            {
                if (rayCast[i].transform.TryGetComponent<AttackUnit>(out AttackUnit e))
                {
                    PoolManager.Instance.GetParticleByType(_skillGameUI.SkillDataSO.ParticleType, _skillPos);
                    e.TakeDamage(_skillGameUI.SkillDataSO.DamageSkill);
                }
            }
        }
    }
}
