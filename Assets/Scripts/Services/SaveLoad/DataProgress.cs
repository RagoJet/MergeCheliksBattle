using System;
using System.Collections.Generic;

namespace Services.SaveLoad
{
    [Serializable]
    public class DataProgress
    {
        public int money = 6000;
        public int levelOfGame = 0;
        public List<CellDTO> cellsDTO = new List<CellDTO>();

        public DataProgress()
        {
            cellsDTO.Add(new CellDTO(6, 0));
            cellsDTO.Add(new CellDTO(9, 0));
        }
    }

    [Serializable]
    public struct CellDTO
    {
        public int indexOfCell;
        public int levelOfCreature;

        public CellDTO(int index, int level)
        {
            indexOfCell = index;
            levelOfCreature = level;
        }
    }
}