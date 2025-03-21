using CrazyPawns.GameAssets.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace CrazyPawns.GameAssets.Pawn.Socket
{
    public class Socket : MonoBehaviour
    {
        public event Action<Socket> OnSelect;
        public event Action<Socket> OnConnect;

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

        private bool _canBeDeleted = false;
        private bool _waitConnection = false;

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

        public void Reinitialize() => CanBeDeleted = false;

        public List<Socket> ConnectedSockets { get; set; } = new();

        public void Highlight(bool highlight)
        {
            _meshRenderer.material = highlight ? _availableMaterial : _defaultMaterial;
            _waitConnection = highlight;
        }

        private void ChangeMaterial() => _meshRenderer.material = _canBeDeleted ? _deleteMaterial : _defaultMaterial;

        private void OnMouseUp()
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
}
