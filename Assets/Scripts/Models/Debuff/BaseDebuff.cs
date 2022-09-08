using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Models
{
    public class BaseDebuff
    {
        public DebuffType DebuffType => _debuffType;
        public float DebuffDuration => _debuffDuration;

        public event Action StartAction;
        public event Action FinishAction;

        [SerializeField] protected DebuffType _debuffType;
        [SerializeField] protected float _debuffDuration;
    }
}
