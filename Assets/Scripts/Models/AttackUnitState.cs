using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class AttackUnitState : MonoBehaviour
    {
        private List<BaseDebuff> _baseDebuff = new List<BaseDebuff>();

        public void AddDebuff(BaseDebuff baseDebuff)
        {
            _baseDebuff.Add(baseDebuff);

            ProccesDebuff();
        }
        public void RemoveDebuff()
        {

        }


        public void ProccesDebuff()
        {
            if (_baseDebuff.Count == 0)
                return;

            for (int i = 0; i < _baseDebuff.Count; i++)
            {
                switch (_baseDebuff[i].DebuffType)
                {
                    case Enums.DebuffType.Frost_Debuff:
                        
                        break;
                    case Enums.DebuffType.Stun_Debuff:

                        break;
                }
            }
        }
    }
}
