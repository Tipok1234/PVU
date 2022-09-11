using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Enums;

namespace Assets.Scripts.DataSo
{
    [System.Serializable]
    public class UnitCharacteristicData
    {
        public float Value => _value;
        public CharacteristicUnitType CharacteristicUnitType => _characteristicUnitType;

        [SerializeField] private float _value;
        [SerializeField] private CharacteristicUnitType _characteristicUnitType;       
    }
}
