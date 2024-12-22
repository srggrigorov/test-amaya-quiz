using System;
using TestAmayaQuiz.Data;
using UnityEngine;

namespace TestAmayaQuiz
{
    public class CellsGeneratorService
    {
        public delegate void OnCellsCreatedDelegate(Cell[] cells);
        public event Action<Cell[]> OnCellsCreated;

        private Transform _cellsContainer;
        private Cell _cellPrefab;

        private Cell[] _cells;
        private LevelChangerService _levelChanger;

        public CellsGeneratorService(LevelChangerService levelChanger, Transform cellsContainer, Cell cellPrefab)
        {
            _cellsContainer = cellsContainer;
            _cellPrefab = cellPrefab;
            _levelChanger = levelChanger;
            _levelChanger.OnLevelChanged += GenerateCells;
        }

        public void GenerateCells(LevelData levelData)
        {
            if (_cells != null && _cells.Length > 0)
            {
                foreach (Cell cell in _cells)
                {
                    GameObject.Destroy(cell.gameObject);
                }
            }

            _cellsContainer.position = Vector3.zero;

            _cells = new Cell[levelData.Rows * levelData.Columns];
            int cellIndex = 0;

            for (int rowIndex = 0; rowIndex < levelData.Rows; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < levelData.Columns; columnIndex++)
                {
                    Cell newCell = GameObject.Instantiate(_cellPrefab, _cellsContainer);
                    Transform cellTransfrom = newCell.transform;
                    cellTransfrom.Translate(
                        Vector3.right * newCell.Size.x * columnIndex * cellTransfrom.localScale.x +
                        Vector3.down * newCell.Size.y * rowIndex * cellTransfrom.localScale.y);

                    _cells[cellIndex] = newCell;
                    cellIndex++;
                }
            }

            _cellsContainer.Translate(_cellsContainer.position -
                (_cells[0].transform.position + _cells[_cells.Length - 1].transform.position) / 2);

            OnCellsCreated?.Invoke(_cells);
        }
    }
}
