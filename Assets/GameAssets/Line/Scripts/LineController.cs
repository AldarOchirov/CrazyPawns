using CrazyPawns.GameAssets.Pawn.Socket;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace CrazyPawns.GameAssets.Line
{
    public class LineController : MonoBehaviour
    {
        [Inject]
        private LinePool _linePool;

        private List<Line> _lines = new();

        public void SpawnLine(Socket firstSocket, Socket secondSocket)
        {
            var line = _linePool.Spawn(new LineConfig(firstSocket, secondSocket));
            line.transform.parent = transform;
            line.UpdateLinePositions();
            _lines.Add(line);
        }

        public void UpdateLinePositions(IReadOnlyList<Socket> sockets)
        {
            var targetLines = _lines.Where(line => 
                sockets.Any(socket => socket == line.LineConfig.StartSocket || socket == line.LineConfig.EndSocket)).ToList();

            targetLines.ForEach(line => line.UpdateLinePositions());
        }
    }
}
