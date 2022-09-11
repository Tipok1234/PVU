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
        public void Setup(UnitCharacteristicData unitCharacteristicData, UnitCharacteristicData unitCharacteristicDatas )
        {
            _typeText.text = unitCharacteristicData.CharacteristicUnitType.ToString();
            _valueText.text =  unitCharacteristicData.Value.ToString() + ("------- ") + unitCharacteristicDatas.Value.ToString();

            //_valueText.text = unitCharacteristicData.Value.ToString();
        }

    }
}
