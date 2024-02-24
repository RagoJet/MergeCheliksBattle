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
            Vector3 startPosition = transform.position +
                                    new Vector3((1 - _collumns) * _offset / 2f, 0, (_rows - 1) * _offset / 2f);
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _collumns; j++)
                {
                    Vector3 pos = startPosition + new Vector3(_offset * j, 0, -_offset * i);
                    Cell cell = gameFactory.CreateCell(pos, transform);
                    _cells.Add(cell);
                }
            }
        }

        public Cell GetCellByIndex(int index)
        {
            return _cells[index];
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