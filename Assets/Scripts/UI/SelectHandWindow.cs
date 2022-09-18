using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.DataSo;
using Assets.Scripts.Enums;
using DG.Tweening;
using UnityEngine.UI;
using System;
using TMPro;

namespace Assets.Scripts.UIManager
{
    public class SelectHandWindow : MonoBehaviour
    {
        [SerializeField] private Transform _spawnHandUnitUI;
        [SerializeField] private Transform _spawnUnitImage;
        [SerializeField] private Transform _spawnShowInit;

        [SerializeField] private Image _mainImage;
        [SerializeField] private TMP_Text _nameUnitText;

        [SerializeField] private HandItem _handItem;
        [SerializeField] private ShowUnitUIItem _showUnitUIItem;
        [SerializeField] private BGImage _bgImage;

        private int _countHandItem = 0;



        private List<HandItem> _handItems = new List<HandItem>();
        private List<ShowUnitUIItem> _showUnitUIItems = new List<ShowUnitUIItem>();

        public void Setup(UnitDataSo[] unitDataSO)
        {
            for (int i = 0; i < unitDataSO.Length; i++)
            {
                _countHandItem++;

                if (_countHandItem > unitDataSO.Length)
                {
                    break;
                }

                HandItem handItem = Instantiate(_handItem, _spawnHandUnitUI);
                _handItems.Add(handItem);

                ShowUnitUIItem showUnit = Instantiate(_showUnitUIItem, _spawnShowInit);

                showUnit.SelectHandUnitAction += OnUnitSelected;
                handItem.DeleteUnitHandActioon += OnDeleteUnitHndAction;

                showUnit.Setup(unitDataSO[i]);

                _showUnitUIItems.Add(showUnit);
            }
        }

        public void OnUnitSelected(ShowUnitUIItem showUnitUIItem)
        {

            _nameUnitText.text = showUnitUIItem.DefenceUnitType.ToString();
            _mainImage.sprite = showUnitUIItem.UnitShowImage;

            for (int i = 0; i < _handItems.Count; i++)
            {
                if (_handItems[i].IsBusy && _handItems[i].DefenceUnitType == showUnitUIItem.DefenceUnitType)
                    return;
            }

            for (int i = 0; i < _handItems.Count; i++)
            {
                if (_handItems[i].IsBusy)
                    continue;

                
                BGImage showUnit = Instantiate(_bgImage, showUnitUIItem.transform.position, Quaternion.identity, _spawnShowInit.parent.parent);

               

                showUnit.Setup(showUnitUIItem.UnitShowImage, showUnitUIItem.DefenceUnitType);

                _handItems[i].SetBusy(true, showUnitUIItem.DefenceUnitType, showUnit.transform);

                showUnit.transform.DOMove(_handItems[i].transform.position, 0.7f).OnComplete(() =>
                {
                    showUnit.transform.SetParent(_handItems[i].transform);
                });

                break;

            }
        }
        public void OnDeleteUnitHndAction(DefenceUnitType defenceUnitType)
        {

        }

    }
}

