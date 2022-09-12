using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Assets.Scripts.DataSo;

namespace Assets.Scripts.UIManager
{
    public class UnitCharacteristicUIItem : MonoBehaviour
    {
        public Image MainImage => _mainImage;

        [SerializeField] private Image _mainImage;

        [SerializeField] private TMP_Text _typeText;
        [SerializeField] private TMP_Text _valueText;


        public void Setup(UnitCharacteristicData current, UnitCharacteristicData next)
        {
            _typeText.text = current.CharacteristicUnitType.ToString();

            if (current.Value == next.Value)
            {

                _valueText.text = current.Value.ToString();
            }
            else
            {
                _valueText.text = current.Value.ToString() + (" >> ") + next.Value.ToString();
            }


            //_valueText.text = unitCharacteristicData.Value.ToString();
        }

        public void SetupLowLevel(UnitCharacteristicData current)
        {
            _typeText.text = current.CharacteristicUnitType.ToString();
            _valueText.text = current.Value.ToString();
        }

    }
}
