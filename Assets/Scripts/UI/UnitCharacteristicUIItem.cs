using UnityEngine;
using TMPro;
using Assets.Scripts.Managers;
using UnityEngine.UI;
using Assets.Scripts.DataSo;
using Assets.SimpleLocalization;

namespace Assets.Scripts.UI
{
    public class UnitCharacteristicUIItem : MonoBehaviour
    {
        public Sprite MainImage => _mainImage.sprite;

        [SerializeField] private Image _mainImage;

        [SerializeField] private TMP_Text _typeText;
        [SerializeField] private TMP_Text _valueText;


        public void Setup(UnitCharacteristicData current, UnitCharacteristicData next)
        {
            _typeText.text = LocalizationManager.Localize("Shop.Characteristic." + current.CharacteristicUnitType);

            if (current.Value == next.Value)
            {
                _valueText.text = current.Value.ToString();
            }
            else
            {
                _valueText.text = current.Value.ToString() + (" >> ") + next.Value.ToString();
            }

            _mainImage.sprite = AssetManager.Instance.GetCharacteristicSprite(current.CharacteristicUnitType);
        }

        public void SetupLowLevel(UnitCharacteristicData current)
        {
            _typeText.text = current.CharacteristicUnitType.ToString();
            _valueText.text = current.Value.ToString();
        }

    }
}
