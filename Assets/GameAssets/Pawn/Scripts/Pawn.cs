using System;
using UnityEngine;

namespace CrazyPawns.GameAssets.Pawn
{
    public class Pawn : MonoBehaviour
    {
        public event Action<Pawn, float> OnMove;

        [SerializeField]
        private PawnMoveDetector _moveDetector;

        [SerializeField]
        private Material _defaultMaterial;

        [SerializeField]
        private Material _deleteMaterial;

        [SerializeField]
        private MeshRenderer _cubeMeshRenderer;

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
            CanBeDeleted = false;
        }

        private void Move(float zDistance)
        {
            OnMove?.Invoke(this, zDistance);
        }

        private void ChangeMaterial() => _cubeMeshRenderer.material = _canBeDeleted ? _deleteMaterial : _defaultMaterial;

        private void OnDestroy()
        {
            _moveDetector.OnMove -= Move;
        }
    }
}
