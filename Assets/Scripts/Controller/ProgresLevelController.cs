using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Managers;
using DG.Tweening;

namespace Assets.Scripts.Controller
{
    public class ProgresLevelController : MonoBehaviour
    {
        [SerializeField] private Slider _progresSlider;
        [SerializeField] private EnemyManager _enemyManager;

        private float _slidingTime = 0.1f;

        private void Awake()
        {
            _progresSlider.value = 0f;

            _enemyManager.ProgresSliderAction += OnUpdateSlider;
        }

        private void OnUpdateSlider(int waveIndex,int waveCount)
        {
            float progress = (float)waveIndex / (float)waveCount;

            _progresSlider.DOValue(progress, _slidingTime);
        }
    }
}
