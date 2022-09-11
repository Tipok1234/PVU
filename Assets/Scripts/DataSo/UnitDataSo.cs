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
        public bool IsOpen => _isOpen;
        public int Price => _price;
        public int HP => _hp;
        public int Level => _level;
        public float PlaceCooldown => _placeCooldown;
        public Sprite UnitSprite => _unitSprite;
        public DefenceUnitType DefencUnitType => _defenceUnitType;
        public UnitCharacteristicData[] UnitCharacteristicDatas => _unitCharacteristicDatas;

        [Header("View")]
        [SerializeField] private Sprite _unitSprite;
        [SerializeField] private int _price;
        [SerializeField] private int _hp;
        [SerializeField] private float _placeCooldown;
        [SerializeField] private DefenceUnitType _defenceUnitType;
        [SerializeField] private UnitCharacteristicData[] _unitCharacteristicDatas;

        private int _level = 1;

        private bool _isOpen;

        public void SetLevel(int level)
        {
            _level = level;
        }
        public void OpenUnit()
        {
            _isOpen = true;
        }
    }
}