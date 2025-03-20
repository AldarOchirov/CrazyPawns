using System;
using UnityEngine;

namespace CrazyPawns.GameAssets.Pawn
{
    public class PawnMoveDetector : MonoBehaviour
    {
        public event Action<float> OnMove;

        private Camera _camera;
        private float _zCameraDistance;

        private void Start()
        {
            _camera = Camera.main;
            _zCameraDistance = _camera.WorldToScreenPoint(transform.position).z;
        }

        private void OnMouseDrag()
        {
            OnMove?.Invoke(_zCameraDistance);
        }
    }
}
