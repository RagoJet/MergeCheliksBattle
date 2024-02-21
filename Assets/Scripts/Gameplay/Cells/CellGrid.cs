using System.Collections.Generic;
using System.Linq;
using Services;
using Services.Factories;
using UnityEngine;

namespace Gameplay.Cells
{
    public class CellGrid : MonoBehaviour
    {
        [SerializeField] private int _rows;
        [SerializeField] private int _collumns;
        [SerializeField] private float _offset;

        private List<Cell> _cells = new List<Cell>();

        private void Awake()
        {
            IGameFactory gameFactory = AllServices.Container.Get<IGameFactory>();
            Vector3 firstCellPos = transform.position;
            for (int row = 0; row < _rows; row++)
            {
                for (int col = 0; col < _collumns; col++)
                {
                    Cell cell = gameFactory.CreateCell(firstCellPos + new Vector3(_offset * col, 0, -_offset * row),
                        transform);
                    _cells.Add(cell);
                }
            }
        }

        public Cell GetCellByIndex(int index)
        {
            return _cells[index];
        }

        public Cell GetAvailableCell()
        {
            return _cells.FirstOrDefault(x => x.currentCreature == null);
        }

        public bool TryGetAvailableCell(out Cell theCell)
        {
            foreach (var cell in _cells)
            {
                if (cell.currentCreature == null)
                {
                    theCell = cell;
                    return true;
                }
            }

            theCell = null;
            return false;
        }
    }
}