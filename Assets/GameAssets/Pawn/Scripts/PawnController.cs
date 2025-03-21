using CrazyPawns.GameAssets.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace CrazyPawns.GameAssets.Pawn
{
    public class PawnController : MonoBehaviour
    {
        [Inject]
        private PawnControllerConfig _config;

        [Inject]
        private PawnPool _pawnPool;

        [Inject]
        private Board.Board _board;

        [Inject]
        private ClickHandler _clickHandler;

        private List<Pawn> _pawns = new();
        private Camera _camera;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            _camera = Camera.main;
            _clickHandler.OnDeactivated += StopHighlightSockets;
            GeneratePawns();
        }

        private void GeneratePawns()
        {
            var positionsInCircle = _board.CellsPositions.Where(pos => CheckDistanceInCircle(pos, _config.InitialZoneRadius)).ToList();
            for (var i = 0; i < _config.InitialPawnCount; i++)
            {
                var pawn = _pawnPool.Spawn(new PawnConfig());
                pawn.transform.parent = transform;
                
                var randomIndex = Random.Range(0, positionsInCircle.Count);
                pawn.transform.position = positionsInCircle[randomIndex];
                pawn.OnMove += OnPawnMove;
                pawn.OnDragEnd += OnPawnDragEnd;
                pawn.OnSocketSelected += SocketSelect;
                _pawns.Add(pawn);
            }
        }

        private bool CheckDistanceInCircle(Vector3 pos, float radius) => pos.x * pos.x + pos.y * pos.y + pos.z * pos.z < radius * radius;

        private void OnPawnMove(Pawn pawn, float zDistance)
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            float distance;
            if (_board.Plane.Raycast(ray, out distance))
            {
                pawn.transform.position = ray.GetPoint(distance);
                pawn.CanBeDeleted = Mathf.Abs(pawn.transform.position.x) > _board.Bound || Mathf.Abs(pawn.transform.position.z) > _board.Bound;
            }
        }

        private void OnPawnDragEnd(Pawn pawn)
        {
            if (pawn.CanBeDeleted)
            {
                RemovePawnListeners(pawn);
                _pawnPool.Despawn(pawn);
                _pawns.Remove(pawn);
                pawn.transform.parent = transform;
            }
        }

        private void SocketSelect(Pawn selectedPawn, Socket.Socket selectedSocket)
        {
            var pawns = _pawns.Where(pawn => pawn != selectedPawn).ToList();
            pawns.ForEach(pawn => pawn.HighlightSockets(true));
        }

        private void StopHighlightSockets() => _pawns.ForEach(pawn => pawn.HighlightSockets(false));

        private void RemovePawnListeners(Pawn pawn)
        {
            pawn.OnMove -= OnPawnMove;
            pawn.OnDragEnd -= OnPawnDragEnd;
            pawn.OnSocketSelected -= SocketSelect;
        }

        private void OnDestroy()
        {
            foreach (var pawn in _pawns)
            {
                RemovePawnListeners(pawn);
            }
            _clickHandler.OnDeactivated -= StopHighlightSockets;
        }
    }
}
