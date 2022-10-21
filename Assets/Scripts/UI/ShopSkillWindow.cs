using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Assets.Scripts.UI
{
    public class ShopSkillWindow : BaseWindow
    {
        [SerializeField] private Button _buySkillButton;
        [SerializeField] private Button _upgradeSkillButton;

        [SerializeField] private TMP_Text _nameSkillText;
        [SerializeField] private TMP_Text _descriptionSkillText;
        [SerializeField] private TMP_Text _hardCurrencyText;
        [SerializeField] private TMP_Text _softCurrencyText;
        [SerializeField] private Image _skillImage;

        private void Awake()
        {
            _buySkillButton.onClick.AddListener(BuySkillOnClick);
            _upgradeSkillButton.onClick.AddListener(UpgradeSkillOnCkick);
            _closeWindowButton.onClick.AddListener(CloseWindow);
        }

        private void BuySkillOnClick()
        {

        }
        private void UpgradeSkillOnCkick()
        {

        }
    }
}