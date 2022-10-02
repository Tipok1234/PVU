using UnityEngine;
using Assets.Scripts.Enums;

namespace Assets.Scripts.DataSo
{
    [CreateAssetMenu(fileName = "UnitData", menuName = "ScriptableObjects/UnitData", order = 1)]
    public class UnitDataSo : ScriptableObject
    {
        public bool IsOpen => _isOpen;
        public int Price => _price;
        public int Level => _level;
        public float PlaceCooldown => _placeCooldown;
        public Sprite UnitSprite => _unitSprite;
        public DefenceUnitType DefencUnitType => _defenceUnitType;

        [Header("View")]
        [SerializeField] private Sprite _unitSprite;
        [SerializeField] private int _price;
        [SerializeField] private float _placeCooldown;
        [SerializeField] private DefenceUnitType _defenceUnitType;
        [SerializeField] private UnitCharacteristicData[] _unitCharacteristicDatas;

        [SerializeField] private int _level;

        [SerializeField] private bool _isOpen;

        public void SetCharacteristicData(UnitCharacteristicData[] data)
        {
            _unitCharacteristicDatas = data;
        }

        public float GetCharacteristicData(CharacteristicUnitType type)
        {
            for (int i = 0; i < _unitCharacteristicDatas.Length; i++)
            {
                if (_unitCharacteristicDatas[i].CharacteristicUnitType == type)
                {
                    return _unitCharacteristicDatas[i].Value;
                }
            }

            Debug.LogError("GetCharacteristicData: " + type + " NOT FOUND");
            return 0;
        }

        public void SetLevel(int level)
        {
            _level = level;
        }

        public void LevelUpUnit()
        {
            _level++;
        }

        public void OpenUnit()
        {
            _isOpen = true;
        }

        public void ResetData()
        {
            _isOpen = false;
            _level = 0;
        }
    }
}