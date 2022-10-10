using UnityEngine;
using Assets.Scripts.Enums;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class BGImage : MonoBehaviour
    {
        [SerializeField] private Image _unitImage;

        private DefenceUnitType _defenceUnitType;
        public void Setup(Sprite sprite, DefenceUnitType defenceUnitType)
        {
            _defenceUnitType = defenceUnitType;
            _unitImage.sprite = sprite;
        }
    }
}