using System;
using UnityEngine;

namespace CrazyPawns.GameAssets.Pawn
{
    public class PawnMoveDetector : MonoBehaviour
    {
        public event Action OnMove;
        public event Action OnMoveFinished;

        private void OnMouseDrag() => OnMove?.Invoke();

        private void OnMouseUp() => OnMoveFinished?.Invoke();
    }
}
