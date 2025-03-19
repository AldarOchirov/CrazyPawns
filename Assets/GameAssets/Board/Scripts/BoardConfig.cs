using UnityEngine;

namespace CrazyPawns.GameAssets.Board
{
    [CreateAssetMenu(menuName = "CrazyPawn/BoardConfig", fileName = "BoardConfig")]
    public class BoardConfig : ScriptableObject
    {
        [SerializeField] 
        private int _checkerboardSize = 18;

        [SerializeField] 
        private Color _blackCellColor = Color.yellow;

        [SerializeField] 
        private Color _whiteCellColor = Color.green;

        public int CheckerBoardSize => _checkerboardSize;

        public Color BlackCellColor => _blackCellColor;

        public Color WhiteCellColor => _whiteCellColor;
    }
}
