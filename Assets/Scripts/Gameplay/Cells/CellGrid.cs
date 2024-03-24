using System.Collections.Generic;
using Configs;
using Gameplay.MergeEntities;
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
            Vector3 startPosition = transform.position +
                                    new Vector3((1 - staticData.Collumns) * staticData.Offset / 2f, 0,
                                        (staticData.Rows - 1) * staticData.Offset / 2f);
            for (int i = 0; i < staticData.Rows; i++)
            {
                for (int j = 0; j < staticData.Collumns; j++)
                {
                    Vector3 pos = startPosition + new Vector3(staticData.Offset * j, 0, -staticData.Offset * i);
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
                if (cell.currentMergeEntity == null)
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
                if (_cells[i].currentMergeEntity != null)
                {
                    MergeEntity mergeEntity = _cells[i].currentMergeEntity;
                    CellDTO cellDto = new CellDTO(mergeEntity.IsRange, i, mergeEntity.Level);
                    newCellsDTO.Add(cellDto);
                }
            }


            SavedData savedData = AllServices.Container.Get<ISaveLoadService>().SavedData;
            savedData.cellsDTO = newCellsDTO;
            AllServices.Container.Get<ISaveLoadService>().SaveProgress();
        }
    }
}