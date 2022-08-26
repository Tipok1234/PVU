using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class ShooterUnit : DefenceUnit
    {
        [SerializeField] private Bullet _bullet;
       
        [SerializeField] private float _speedBullet;

        private bool _isTarget;

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.S))
            {
                Instantiate(_bullet, transform.position, _bullet.transform.rotation).Setup();

                var ray = new Ray(transform.position, transform.forward);
                RaycastHit hit;

                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
                }
            }
        }
    }
}
