using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.DataSo;
using Assets.Scripts.Enums;
using DG.Tweening;
using UnityEngine.UI;
using System;
using TMPro;
using Assets.Scripts.Managers;
using Assets.Scripts.Config;
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
        [SerializeField] private MapWindow _mapWindow;
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

                        bgImage.Setup(unitDataSO[i].UnitSprite);

                        hand.SetBusy(true, unitDataSO[i].DefencUnitType, bgImage.transform);

                        Debug.LogError(unitHandItems.Count);
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

                _nameUnitText.text = LocalizationManager.Localize(LocalizationConst.DefenceUnits + unitDataSO[1].DefencUnitType);
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
                _mapWindow.OpenWindow();
                CloseWindow();
            }
        }

        public void OnUnitSelected(ShowUnitUIItem showUnitUIItem)
        {
            _nameUnitText.text = LocalizationManager.Localize(LocalizationConst.DefenceUnits + showUnitUIItem.DefenceUnitType);
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

                    showUnit.Setup(showUnitUIItem.UnitShowImage);
                    showUnit.MoveAnimation(_handItems[i].transform);

                    _handItems[i].SetBusy(true, showUnitUIItem.DefenceUnitType, showUnit.transform);

                    AudioManager.Instance.PlaySoundGame(AudioSoundType.NoMoneySound);

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

        public override void CloseWindow()
        {
            base.CloseWindow();

            for (int i = 0; i < _showUnitUIItems.Count; i++)
            {
                Destroy(_showUnitUIItems[i].gameObject);
            }

            for (int i = 0; i < _handItems.Count; i++)
            {
                _handItems[i].ResetElement();
            }

            _showUnitUIItems.Clear();
        }
        public override void OpenWindow()
        {
            base.OpenWindow();

            Setup(ConfigManager.Intsance.Config.UnitDataSos, DataManager.Instance.UnitHandItems);
        }
    }
}

