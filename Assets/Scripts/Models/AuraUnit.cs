using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Models
{
    public class AuraUnit : AttackUnit
    {
        [SerializeField] private LayerMask _auraLayer;
        [SerializeField] private float _auraValue;

        private Collider[] _allyUnitsCollider;
        private List<AttackUnit> _affectedUnits = new List<AttackUnit>();

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            _allyUnitsCollider = Physics.OverlapSphere(_selfTransform.position, 2f, _auraLayer);

            for (int i = 0; i < _allyUnitsCollider.Length; i++)
            {
                if (_allyUnitsCollider[i].transform.TryGetComponent<AttackUnit>(out AttackUnit e))
                {
                    if (_affectedUnits.Contains(e) || e.IsAuraImmunity)
                        continue;

                    _affectedUnits.Add(e);
                    e.AuraSpeed(_auraValue);
                }
            }

            if (_affectedUnits.Count > 0 && _allyUnitsCollider.Length-1 != _affectedUnits.Count)
            {
                for (int i = 0; i < _affectedUnits.Count; i++)
                {
                    if (_allyUnitsCollider[i].transform.TryGetComponent<AttackUnit>(out AttackUnit e))
                    {
                        if (_affectedUnits.Contains(e) == false)
                        {
                            _affectedUnits[i].RetrunSpeedUnit();
                            _affectedUnits.RemoveAt(i);
                            i = 0;
                        }
                    }

                }
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_selfTransform.position, 2f);
        }
        //public override void MoveUnit()
        //{
        //    base.MoveUnit();
        //}
    }
}
