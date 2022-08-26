using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _speedBullet;
        [SerializeField] private float _bulletLifeTime;
        private float _currentTimeBullet = 0;

        private bool _isActive;

        private void Update()
        {
            if(_isActive)
            {
                transform.Translate(_speedBullet * Time.deltaTime * Vector3.back);

                _currentTimeBullet += Time.deltaTime;

                if (_currentTimeBullet >= _bulletLifeTime)
                {
                    _isActive = false;
                    _currentTimeBullet = 0;
                    gameObject.SetActive(false);
                }
            }
        }

        public void Setup()
        {
            _isActive = true;
        }
    }
}
