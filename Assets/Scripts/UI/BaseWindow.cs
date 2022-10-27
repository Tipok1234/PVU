using UnityEngine.UI;
using UnityEngine;
using System;
using Assets.Scripts.Managers;
using Assets.Scripts.Enums;
using Assets.Scripts.DataSo;

namespace Assets.Scripts.UI
{
    public class BaseWindow : MonoBehaviour
    {
        public event Action<BaseWindow> OpenWindowAction;

        public event Action<BaseWindow> CloseWindowAction;

        public WindowType WindowType => _windowType;

        [SerializeField] protected Canvas _selfCanvas;
        [SerializeField] protected Button _closeWindowButton;
        [SerializeField] protected WindowType _windowType;

        private void Awake()
        {
            _closeWindowButton.onClick.AddListener(CloseWindow);
        }

        public virtual void OpenWindow()
        {
            AudioManager.Instance.PlaySoundGame(AudioSoundType.OpenWindowSound);
            _selfCanvas.enabled = true;
            OpenWindowAction?.Invoke(this);
        }

        public virtual void CloseWindow()
        {
            AudioManager.Instance.PlaySoundGame(AudioSoundType.OpenWindowSound);
            _selfCanvas.enabled = false;
            CloseWindowAction?.Invoke(this);
        }
    }

}
