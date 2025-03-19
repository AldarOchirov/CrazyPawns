using CrazyPawns.GameAssets.Cell;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace CrazyPawns.GameAssets.Board
{
    public class Board : MonoBehaviour
    {
        [Inject]
        private BoardConfig _config;

        [Inject]
        private CellPool _cellPool;

        [SerializeField]
        private Material _blackCellMaterial;

        [SerializeField]
        private Material _whiteCellMaterial;

        private Cell.Cell[,] _cells;

        public List<Vector3> CellsPositions { get; private set; } = new();

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            GenerateCells();
        }

        private void GenerateCells()
        {
            var boardSize = _config.CheckerBoardSize;
            _cells = new Cell.Cell[boardSize, boardSize];
            for (var i = 0; i < boardSize; i++)
            {
                for (var j = 0; j < boardSize; j++)
                {
                    var cell = _cellPool.Spawn(new CellConfig((i + j) % 2 == 0 ? _blackCellMaterial : _whiteCellMaterial));
                    var xPos = CalcPos(i);
                    var zPos = CalcPos(j);
                    cell.transform.position = new Vector3(xPos, 0.0f, zPos);
                    CellsPositions.Add(cell.transform.position);
                    cell.transform.parent = transform;
                    _cells[i,j] = cell;
                }
            }
        }

        private float CalcPos(int index)
        {
            var shift = _config.CellSize % 2 == 0 ? 0.0f : _config.CellSize / 2;
            return shift - ((float)_config.CheckerBoardSize / 2 - index) * _config.CellSize;
        }
    }
}
