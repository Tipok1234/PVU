using Assets.Scripts.UI;
using Assets.Scripts.Managers;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class FrostSkill : BaseSkill
    {
        [SerializeField] private Transform _skillPos;
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private SkillGameUI _skillGameUI;
        [SerializeField] private float _stopTimeSpeed;

        public override void UseSkill()
        {
            _skillGameUI.RechargeTimeSkill();
            var rayCast = Physics.OverlapSphere(new Vector3(0, 0, 0), 14f, _enemyLayer);
            Debug.LogError("SKILL:");

            for (int i = 0; i < rayCast.Length; i++)
            {
                if (rayCast[i].transform.TryGetComponent<AttackUnit>(out AttackUnit e))
                {
                    PoolManager.Instance.GetParticleByType(_skillGameUI.SkillDataSO.ParticleType, _skillPos);
                    e.TakeSkill();

                    StartCoroutine(ReturnSkill(e));
                }
            }
        }

        private IEnumerator ReturnSkill(AttackUnit attackUnit)
        {
            yield return new WaitForSeconds(_stopTimeSpeed);

            attackUnit.ReturnToUnit();
        }
    }
}
