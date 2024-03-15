using System.Collections.Generic;
using Configs;
using Gameplay.Units.Creatures;
using Services;
using Services.Factories;
using Services.SaveLoad;
using UnityEngine;

namespace Gameplay.Cells
{
    public class CellGrid : MonoBehaviour
    {
        private List<Cell> _cells = new List<Cell>();

        private void Awake()
        {
            IGameFactory gameFactory = AllServices.Container.Get<IGameFactory>();
            IStaticDataFactory dataFactory = AllServices.Container.Get<IStaticDataFactory>();
            GridStaticData staticData = dataFactory.GetGridStaticData();
            int _collumns = staticData.Collumns;
            int _rows = staticData.Rows;
            float _offset = staticData.Offset;
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

        public void SetCellsInfo()
        {
            List<CellDTO> newCellsDTO = new List<CellDTO>();

            for (int i = 0; i < _cells.Count; i++)
            {
                if (_cells[i].currentCreature != null)
                {
                    Creature creature = _cells[i].currentCreature;
                    CellDTO cellDto = new CellDTO(creature.IsRange, i, creature.Level);
                    newCellsDTO.Add(cellDto);
                }
            }


            SavedData savedData = AllServices.Container.Get<ISaveLoadService>().SavedData;
            savedData.cellsDTO = newCellsDTO;
        }
    }
}