using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Assets.Scripts.UI
{
    public class BGImage : MonoBehaviour
    {
        [SerializeField] private Image _unitImage;

        private Sequence _sequence;
        public void Setup(Sprite sprite)
        {
            _unitImage.sprite = sprite;
        }

        public void MoveAnimation(Transform newTransform)
        {
            _sequence = DOTween.Sequence();
            _sequence.Append(transform.DOMove(newTransform.position, 0.7f));
            _sequence.AppendCallback(()=>transform.SetParent(newTransform.transform));
        }

        private void OnDestroy()
        {
            _sequence?.Kill();
        }
    }
}