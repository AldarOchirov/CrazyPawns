using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CrazyPawns.GameAssets.Pawn
{
    public class Pawn : MonoBehaviour
    {
        public event Action<Pawn, float> OnMove;
        public event Action<Pawn> OnDragEnd;
        public event Action<Pawn, Socket.Socket> OnSocketSelected;

        [SerializeField]
        private PawnMoveDetector _moveDetector;

        [SerializeField]
        private Material _defaultMaterial;

        [SerializeField]
        private Material _deleteMaterial;

        [SerializeField]
        private MeshRenderer _cubeMeshRenderer;

        [SerializeField]
        private List<Socket.Socket> _sockets;

        private bool _canBeDeleted = false;

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

        public void Reinitialize(PawnConfig config)
        {
            _moveDetector.OnMove += Move;
            _moveDetector.OnDragEnd += DragEnd;
            CanBeDeleted = false;
            foreach (var socket in _sockets)
            {
                socket.Reinitialize();
                socket.OnSelect += SocketSelected;
            }
        }

        private void Move(float zDistance) => OnMove?.Invoke(this, zDistance);

        private void DragEnd() => OnDragEnd?.Invoke(this);

        private void ChangeMaterial()
        {
            _cubeMeshRenderer.material = _canBeDeleted ? _deleteMaterial : _defaultMaterial;
            _sockets.ForEach(socket => socket.CanBeDeleted = _canBeDeleted);
        }

        private void SocketSelected(Socket.Socket socket) => OnSocketSelected?.Invoke(this, socket);

        public void HighlightSockets(bool highlight) => _sockets.ForEach(socket => socket.Highlight(highlight));

        private void OnDestroy()
        {
            _moveDetector.OnMove -= Move;
            _moveDetector.OnDragEnd -= DragEnd;
            _sockets.ForEach(socket => socket.OnSelect -= SocketSelected);
        }
    }
}
