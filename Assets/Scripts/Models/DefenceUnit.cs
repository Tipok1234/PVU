using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class DefenceUnit : BaseUnit
    {
        
        public virtual int Sell()
        {
            base.Death();
            return 10;
        }
    }
}
