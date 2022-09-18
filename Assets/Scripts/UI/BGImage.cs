using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Enums;
using Assets.Scripts.DataSo;
using UnityEngine.UI;
using System;

public class BGImage : MonoBehaviour
{
   // public event Action<BGImage> SelectHandUnitAction;

    public DefenceUnitType DefenceUnitType => _defenceUnitType;
    public Sprite UnitImage => _unitImage.sprite;

    [SerializeField] private Image _unitImage;

    private DefenceUnitType _defenceUnitType;


    public void Setup(Sprite sprite, DefenceUnitType defenceUnitType)
    {
        _defenceUnitType = defenceUnitType;
        _unitImage.sprite = sprite;
    }
}