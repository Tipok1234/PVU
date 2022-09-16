using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.DataSo;
using Assets.Scripts.Enums;
using DG.Tweening;
using System;

namespace Assets.Scripts.UIManager
{
    public class SelectHandWindow : MonoBehaviour
    {
        [SerializeField] private Transform _spawnHandUnitUI;
        [SerializeField] private Transform _spawnUnitImage;
        [SerializeField] private Transform _spawnShowInit;

        [SerializeField] private HandItem _handItem;
        [SerializeField] private ShowUnitUIItem _showUnitUIItem;
        [SerializeField] private BGImage _bgImage;

        private int _countHandItem = 0;

        private List<HandItem> _handItems = new List<HandItem>();
        private List<ShowUnitUIItem> _showUnitUIItems = new List<ShowUnitUIItem>();
        private List<BGImage> _bGImages = new List<BGImage>();

        public void Setup(UnitDataSo[] unitDataSO)
        {
            for (int i = 0; i < unitDataSO.Length; i++)
            {
                _countHandItem++;

                if (_countHandItem > 8)
                {
                    break;
                }

                HandItem handItem = Instantiate(_handItem, _spawnHandUnitUI);
                _handItems.Add(handItem);

                ShowUnitUIItem showUnit = Instantiate(_showUnitUIItem, _spawnShowInit);

                showUnit.SelectHandUnitAction += OnUnitSelected;
                handItem.DeleteUnitHandActioon += OnDeleteUnitHndAction;

                showUnit.Setup(unitDataSO[i]);
                _bgImage.Setup(unitDataSO[i]);

                _showUnitUIItems.Add(showUnit);
            }
        }

        public void OnUnitSelected(ShowUnitUIItem showUnitUIItem)
        {
            for (int i = 0; i < _handItems.Count; i++)
            {
                if (_handItems[i].IsBusy)
                    continue;



                BGImage showUnit = Instantiate(_bgImage, showUnitUIItem.transform.position,Quaternion.identity ,_spawnShowInit.parent.parent);

              //  showUnit.UnitImage = showUnitUIItem.UnitShowImage;

                _handItems[i].SetBusy(true,DefenceUnitType.DoubleShooter_Unit, showUnit.transform);
                showUnit.transform.DOMove(_handItems[i].transform.position, 0.7f).OnComplete(() =>
                {
                    showUnit.transform.SetParent(_handItems[i].transform);
                });
                break;
            }
        }
        public void OnDeleteUnitHndAction(DefenceUnitType defenceUnitType)
        {
            // ShowUnitUIItem showUnit = Instantiate(_showUnitUIItem, _spawnHandUnitUI);

            //for (int i = 0; i < _handItems.Count; i++)
            //{
            //    if (_handItems[i].DefenceUnitType == defenceUnitType)
            //    {
            //        _handItems[i].SetBusy(false);
            //        break;
            //    }

            //}
    
            // handItem.SetBusy(false);
            //  handItem.SetBusy(false);

            //handItem.SetBusy(false);

            //for (int i = 0; i < _handItems.Count; i++)
            //{
            //    if (!_handItems[i].IsBusy)
            //        continue;

            //    _handItems[i].SetBusy(false);

            //    //for (int j = 0; j < _showUnitUIItems.Count; j++)
            //    //{

            //    //    _handItems[i].SetBusy(false);
            //    //}
            //}

            //HandItem handItem = Instantiate(_handItem, _spawnHandUnitUI);
            //        _handItems[i].gameObject.SetActive(false);

            //        //Destroy(_handItems[i]);
            //        //showUnit.transform.DOMove(_showUnitUIItems[i].transform.position, 0.7f).OnComplete(() =>
            //        //{
            //        //    showUnit.transform.SetParent(_showUnitUIItems[i].transform);


            //        //});
            //    }

            //    break;
            //}
        }
        //else if (_index >= 8)
        //{
        //    ShowUnitUIItem showUnit = Instantiate(_showUnitUIItem, _spawnHandUnitUI);
        //    Debug.LogError("!!!!!");
        //    showUnit.transform.DOMove(_showUnitUIItems[_index].transform.position, 0.7f).OnComplete(() => showUnit.transform.SetParent(_showUnitUIItems[_index].transform));
        //    _index--;
        //    _isBusy = false;
        //}





    }
}

