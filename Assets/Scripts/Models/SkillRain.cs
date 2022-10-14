using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Enums;
using Assets.Scripts.Managers;
using TMPro;
using System;

namespace Assets.Scripts.Models
{
    public class SkillRain : MonoBehaviour
    {
        public event Action<SkillType,float> SkillAction;

        [SerializeField] private Transform _skillPosition;
        [SerializeField] private Button _castSkillButton;
        [SerializeField] private TMP_Text _costSkill;
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private ParticleType _particleType;
        [SerializeField] private SkillType _skillType;
        [SerializeField] private float _damage;
        [SerializeField] private Image _fillImage;
        [SerializeField] private Color _noMoneyColor;
        [SerializeField] private Color _normalColor;

        private float _priceSkill = 100;

        private void Awake()
        {
            _castSkillButton.onClick.AddListener(CastSkillOnClick);
        }
        private void Start()
        {
            _costSkill.text = _priceSkill.ToString();
        }
        public void CastSkillOnClick()
        {
            StartCoroutine(SkillCoroutine());
        }

        private IEnumerator SkillCoroutine()
        {
            yield return new WaitForSeconds(0.5f);
            RechargeTimeSkill();
            SkillAction?.Invoke(_skillType, _priceSkill);

            var rayCast = Physics.OverlapSphere(new Vector3(0,0,0), 14f, _enemyLayer);

            for (int i = 0; i < rayCast.Length; i++)
            {
                if (rayCast[i].transform.TryGetComponent<AttackUnit>(out AttackUnit e))
                {
                    PoolManager.Instance.GetParticleByType(_particleType, _skillPosition);
                    e.TakeDamage(_damage);
                }
            }
        }

        public void RechargeTimeSkill()
        {
            _castSkillButton.enabled = false;
            _fillImage.fillAmount = 1;
            _fillImage.DOFillAmount(0f, 5f).SetEase(Ease.Linear).OnComplete(() => _castSkillButton.enabled = true);
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(new Vector3(0, 0, 0),14f);
        }
    }
}
