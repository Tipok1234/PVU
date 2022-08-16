using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Enums;

namespace Assets.Scripts.DataSo
{
    [CreateAssetMenu(fileName = "UnitShopData", menuName = "ScriptableObjects/UnitShopData", order = 1)]
    public class UnitDataSo : MonoBehaviour
    {
        public int Price => _price;
        public Image UnitImage => _unitImage;
        public TypeUnit TypeUnit => _typeUnit;

        [Header("View")]
        [SerializeField] private Image _unitImage;
        [SerializeField] private int _price;
       //health
        [SerializeField] private TypeUnit _typeUnit;
    }
}