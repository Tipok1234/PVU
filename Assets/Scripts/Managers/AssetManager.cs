using UnityEngine;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Managers
{
    public class AssetManager : MonoBehaviour
    {
        [SerializeField] private SpriteUnitCharacteristicData[] _spriteUnitCharacteristicDatas;
        public static AssetManager Instance
        {
            get
            {
                return _instance;
            }
        }

        private static AssetManager _instance;

        private void Awake()
        {
            if (_instance == null)
                _instance = this;
        }

        public Sprite GetCharacteristicSprite(CharacteristicUnitType characteristicUnitType)
        {
            for (int i = 0; i < _spriteUnitCharacteristicDatas.Length; i++)
            {
                SpriteUnitCharacteristicData unit = _spriteUnitCharacteristicDatas[i];

                if (unit.CharacteristicUnitType == characteristicUnitType)
                {
                    return unit.UnitCharacteristicSprite;
                }
            }

            Debug.LogError("Config not found");
            return null;
        }
    }

    [System.Serializable]
    public struct SpriteUnitCharacteristicData
    {
        public CharacteristicUnitType CharacteristicUnitType => _characteristicUnitType;
        public Sprite UnitCharacteristicSprite => _unitSprite;

        [SerializeField] private CharacteristicUnitType _characteristicUnitType;
        [SerializeField] private Sprite _unitSprite;
    }
}
