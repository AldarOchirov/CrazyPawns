using CrazyPawns.GameAssets.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace CrazyPawns.GameAssets.Pawn.Socket
{
    public class Socket : MonoBehaviour
    {
        private const float ClickDeltaTime = 0.2f;
        private static bool IsDragging = false;

        public event Action<Socket> OnSelect;
        public event Action<Socket> OnConnect;
        public event Action OnDeactivateHighlight;

        [Inject]
        private ClickHandler _clickHandler;

        [SerializeField]
        private MeshRenderer _meshRenderer;

        [SerializeField]
        private Material _defaultMaterial;

        [SerializeField]
        private Material _deleteMaterial;

        [SerializeField]
        private Material _availableMaterial;

        private Camera _camera;
        private bool _canBeDeleted = false;
        private bool _waitConnection = false;

        private float _downClickTime;

        public bool CanBeDeleted
        {
            get => _canBeDeleted;
            set
            {
                if (_canBeDeleted != value)
                {
                    _canBeDeleted = value;
                    ChangeMaterial();
                }
            }
        }

        private void Start()
        {
            _camera = Camera.main;
            _clickHandler.OnDeactivated += DeactivateHighlight;
        }

        public void Reinitialize() => CanBeDeleted = false;

        public void Highlight(bool highlight)
        {
            _meshRenderer.material = highlight ? _availableMaterial : _defaultMaterial;
            _waitConnection = highlight;
        }

        private void ChangeMaterial() => _meshRenderer.material = _canBeDeleted ? _deleteMaterial : _defaultMaterial;

        private void DeactivateHighlight() => OnDeactivateHighlight?.Invoke();

        private void OnMouseDown() => _downClickTime = Time.time;

        private void OnMouseDrag()
        {
            if (Time.time - _downClickTime >= ClickDeltaTime)
            {
                IsDragging = true;
                OnSelect?.Invoke(this);
            }
        }

        private void OnMouseUp()
        {
            if (IsDragging)
            {
                var ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out var hit) && hit.transform.TryGetComponent<Socket>(out var socket))
                {
                    OnConnect?.Invoke(socket);
                }

                OnDeactivateHighlight?.Invoke();
                IsDragging = false;
            }
            else
            {
                if (_waitConnection)
                {
                    OnConnect?.Invoke(this);
                }

                if (!_clickHandler.IsActive)
                {
                    _clickHandler.ActivateButton();
                    OnSelect?.Invoke(this);
                }
            }
        }

        private void OnDestroy() => _clickHandler.OnDeactivated -= DeactivateHighlight;
    }
}
