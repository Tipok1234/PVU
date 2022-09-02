using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Enums;

namespace Assets.Scripts.DataSo
{
    [CreateAssetMenu(fileName = "UnitData", menuName = "ScriptableObjects/UnitData", order = 1)]
    public class UnitDataSo : ScriptableObject
    {
        public int Price => _price;
        public int HP => _hp;
        public float PlaceCooldown => _placeCooldown;
        public Sprite UnitSprite => _unitSprite;
        public DefenceUnitType DefencUnitType => _defenceUnitType;

        [Header("View")]
        [SerializeField] private Sprite _unitSprite;
        [SerializeField] private int _price;
        [SerializeField] private int _hp;
        [SerializeField] private float _placeCooldown;
        [SerializeField] private DefenceUnitType _defenceUnitType;
    }
}