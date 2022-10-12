using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.DataSo;
using Assets.Scripts.Enums;
using DG.Tweening;
using UnityEngine.UI;
using System;
using TMPro;
using Assets.Scripts.Managers;
using UnityEngine.SceneManagement;
using Assets.SimpleLocalization;
using System.Linq;

namespace Assets.Scripts.UI
{
    public class SelectHandWindow : BaseWindow
    {
        public event Action<DefenceUnitType> SaveHandItemAction;
        public event Action<DefenceUnitType> RemoveHandItemAction;

        [SerializeField] private Transform _spawnHandUnitUI;
        [SerializeField] private Transform _spawnUnitImage;
        [SerializeField] private Transform _spawnShowInit;

        [SerializeField] private Image _mainImage;
        [SerializeField] private TMP_Text _nameUnitText;
        [SerializeField] private TMP_Text _lockText;

        [SerializeField] private Button _fightButton;
        [SerializeField] private PopUp _popUpCanvas;
        [SerializeField] private HandItem _handItem;
        [SerializeField] private ShowUnitUIItem _showUnitUIItem;
        [SerializeField] private BGImage _bgImage;

        private int _countHandItem = 0;

        private List<HandItem> _handItems = new List<HandItem>();
        private List<ShowUnitUIItem> _showUnitUIItems = new List<ShowUnitUIItem>();

        private void Awake()
        {
            _fightButton.onClick.AddListener(CheckHandItems);
            _closeWindowButton.onClick.AddListener(CloseWindow);

            //LocalizationManager.LocalizationChanged += OnLangueageChanged;
        }
        public void Setup(UnitDataSo[] unitDataSO, List<DefenceUnitType> unitHandItems)
        {
            for (int i = 0; i < unitDataSO.Length; i++)
            {
                if (_countHandItem < 8)
                {
                    HandItem handItem = Instantiate(_handItem, _spawnHandUnitUI);
                    _handItems.Add(handItem);
                    handItem.DeleteUnitHandActioon += OnDeleteUnitHndAction;
                }

                if (unitHandItems.Contains(unitDataSO[i].DefencUnitType))
                {
                    HandItem hand = _handItems.FirstOrDefault(h => !h.IsBusy);

                    if (hand != null)
                    {
                        BGImage bgImage = Instantiate(_bgImage, hand.transform.position, Quaternion.identity, hand.transform);

                        bgImage.Setup(unitDataSO[i].UnitSprite, unitDataSO[i].DefencUnitType);

                        hand.SetBusy(true, unitDataSO[i].DefencUnitType, bgImage.transform);
                    }
                }

                ShowUnitUIItem showUnit = Instantiate(_showUnitUIItem, _spawnShowInit);

                showUnit.SelectHandUnitAction += OnUnitSelected;

                if (unitDataSO[i].IsOpen)
                {
                    showUnit.transform.SetAsFirstSibling();
                }

                showUnit.Setup(unitDataSO[i]);

                _showUnitUIItems.Add(showUnit);

                _nameUnitText.text = LocalizationManager.Localize("DefenceUnits." + unitDataSO[1].DefencUnitType);
                //_nameUnitText.text = unitDataSO[1].DefencUnitType.ToString();
                _mainImage.sprite = unitDataSO[1].UnitSprite;

                _countHandItem++;
            }
        }

        public void CheckHandItems()
        {
            if (!_handItems[0].IsBusy)
            {
                _popUpCanvas.OpenWindow();
            }
            else
            {
                SceneManager.LoadScene("GameScene");
                OpenWindow();
            }
        }

        //public void RefreshSelectHandWindow(UnitDataSo[] unitDataSO)
        //{
        //    for (int i = 0; i < unitDataSO.Length; i++)
        //    {
        //        if (unitDataSO[i].IsOpen)
        //        {
        //            _showUnitUIItems[i].transform.SetAsFirstSibling();
        //        }
        //    }
        //}

        //public void RemoveShowUnit(UnitDataSo[] unitDataSo)
        //{
        //    for (int i = 0; i < unitDataSo.Length; i++)
        //    {
        //        if (unitDataSo[i].DefencUnitType == _showUnitUIItem.DefenceUnitType)
        //        {
        //            _showUnitUIItems.Remove(_showUnitUIItem);
        //        }
        //    }       
        //}

        public void OnUnitSelected(ShowUnitUIItem showUnitUIItem)
        {
            _nameUnitText.text = LocalizationManager.Localize("DefenceUnits." + showUnitUIItem.DefenceUnitType);
            //_nameUnitText.text = showUnitUIItem.DefenceUnitType.ToString();
            _mainImage.sprite = showUnitUIItem.UnitShowImage;

            if (showUnitUIItem.IsOpenImage)
            {
                _lockText.gameObject.SetActive(false);

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

                    AudioManager.Instance.PlaySoundGame(AudioSoundType.NoMoneySound);

                    showUnit.transform.DOMove(_handItems[i].transform.position, 0.7f).OnComplete(() =>
                    {
                        showUnit.transform.SetParent(_handItems[i].transform);
                    });

                    SaveHandItemAction?.Invoke(_handItems[i].DefenceUnitType);

                    break;
                }
            }
            else
            {
                _lockText.gameObject.SetActive(true);
            }
        }
        public void OnDeleteUnitHndAction(DefenceUnitType defenceUnitType)
        {
            RemoveHandItemAction?.Invoke(defenceUnitType);
        }
        //public void OnLangueageChanged()
        //{
        //    _nameUnitText.text = LocalizationManager.Localize("DefenceUnits." + _showUnitUIItem.DefenceUnitType);
        //}
    }
}

