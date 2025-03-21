using CrazyPawns.GameAssets.Pawn.Socket;
using UnityEngine;

namespace CrazyPawns.GameAssets.Line
{
    public struct LineConfig
    {
        public Socket StartSocket;
        public Socket EndSocket;

        public LineConfig(Socket startSocket, Socket endSocket)
        {
            StartSocket = startSocket;
            EndSocket = endSocket;
        }
    }
}
