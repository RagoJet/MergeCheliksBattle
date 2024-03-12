using System;
using System.Collections.Generic;

namespace Services.SaveLoad
{
    [Serializable]
    public class DataProgress
    {
        public int money = 60000;
        public int levelOfGame = 0;
        public List<CellDTO> cellsDTO = new List<CellDTO>();

        public DataProgress()
        {
            cellsDTO.Add(new CellDTO(false, 6, 0));
            cellsDTO.Add(new CellDTO(true, 9, 0));
        }
    }

    [Serializable]
    public struct CellDTO
    {
        public int indexOfCell;
        public int levelOfCreature;
        public bool isRange;

        public CellDTO(bool range, int index, int level)
        {
            isRange = range;
            indexOfCell = index;
            levelOfCreature = level;
        }
    }
}