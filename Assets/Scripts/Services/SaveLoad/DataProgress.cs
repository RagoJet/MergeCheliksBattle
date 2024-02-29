using System;
using System.Collections.Generic;

namespace Services.SaveLoad
{
    [Serializable]
    public class DataProgress
    {
        public int money = 600;
        public int levelOfGame = 10;
        public List<CellDTO> _cellsDTO = new List<CellDTO>();

        public DataProgress()
        {
            _cellsDTO.Add(new CellDTO(6, 0));
            _cellsDTO.Add(new CellDTO(9, 0));
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