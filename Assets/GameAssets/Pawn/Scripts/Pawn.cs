using System;
using UnityEngine;

namespace CrazyPawns.GameAssets.Pawn
{
    public class Pawn : MonoBehaviour
    {
        public event Action<Pawn, float> OnMove;

        [SerializeField]
        private PawnMoveDetector _moveDetector;

        public void Reinitialize(PawnConfig config)
        {
            _moveDetector.OnMove += Move;
        }

        private void Move(float zDistance)
        {
            OnMove?.Invoke(this, zDistance);
        }

        private void OnDestroy()
        {
            _moveDetector.OnMove -= Move;
        }
    }
}
