using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class GunPowderModel : MonoBehaviour
    {
        public int SoftCurrency => _softCurrency;

        [SerializeField] private int _softCurrency;
    }
}
