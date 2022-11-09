using UnityEngine;
using System;
using System.Collections;

namespace Assets.Scripts.Models
{
    public class GameOver : MonoBehaviour
    {
        public event Action RestartGameAction;
        [SerializeField] protected LayerMask _enemyLayer;

        private bool _isGameOver = false;

        public void ResetGameOver()
        {
            _isGameOver = true;
        }

        private void FixedUpdate()
        {
            if (_isGameOver)
                return;

            var ray = new Ray(transform.position, transform.right * (6.5f));

            Debug.DrawRay(transform.position, transform.right * (6.5f), Color.red, Time.deltaTime);

            if (Physics.Raycast(ray, out RaycastHit hit, 10f, _enemyLayer))
            {
                StartCoroutine(GameOverCoroutine());
                _isGameOver = true;
            }
        }

        private IEnumerator GameOverCoroutine()
        {
            yield return new WaitForSeconds(2.0f);
            RestartGameAction?.Invoke();
        }
    }
}