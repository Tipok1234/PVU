using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Models
{
    public class BaseDebuff : MonoBehaviour
    {
        [SerializeField] protected DebuffType _debuffType;
        [SerializeField] protected float _speedDecreasePercent;
        [SerializeField] protected float _debuffDuration;
    }
}
