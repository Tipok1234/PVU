using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Assets.Scripts.Models
{
    public class GameOver : MonoBehaviour
    {
        public event Action RestartGameAction;
        [SerializeField] protected LayerMask _enemyLayer;
        private void FixedUpdate()
        {
            var ray = new Ray(transform.position, transform.right * (6.5f));

            Debug.DrawRay(transform.position, transform.right * (6.5f), Color.red, Time.deltaTime);

            if (Physics.Raycast(ray, out RaycastHit hit, 10f, _enemyLayer))
            {
                if (hit.transform.TryGetComponent<AttackUnit>(out AttackUnit enemy))
                {
                    RestartGameAction?.Invoke();
                }
            }
        }
    }
}