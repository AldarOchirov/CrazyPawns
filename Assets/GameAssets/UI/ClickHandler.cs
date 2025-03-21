using System;
using UnityEngine;
using UnityEngine.UI;

namespace CrazyPawns.GameAssets.UI
{
    public class ClickHandler : MonoBehaviour
    {
        public event Action OnDeactivated;

        [SerializeField]
        private Button _button;

        public bool IsActive
        {
            get => _button.gameObject.activeSelf;
            private set => _button.gameObject.SetActive(value);
        }

        private void Start()
        {
            DeactivateButton();
            _button.onClick.AddListener(DeactivateButton);
        }

        public void ActivateButton() => IsActive = true;

        private void DeactivateButton()
        {
            IsActive = false;
            OnDeactivated?.Invoke();
        }

        private void OnDestroy() => _button.onClick?.RemoveAllListeners();
    }
}
