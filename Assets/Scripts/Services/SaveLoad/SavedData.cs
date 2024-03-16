using System;
using System.Collections.Generic;

namespace Services.SaveLoad
{
    [Serializable]
    public class SavedData
    {
        public int gold;
        public int levelOfGame;
        public List<CellDTO> cellsDTO = new List<CellDTO>();

        public DTODateTime dateTimeExpirationSub;


        public SavedData()
        {
            gold = 400;
            levelOfGame = 1;

            cellsDTO.Add(new CellDTO(false, 5, 0));
            cellsDTO.Add(new CellDTO(false, 6, 0));
            cellsDTO.Add(new CellDTO(true, 9, 0));
            cellsDTO.Add(new CellDTO(true, 10, 0));

            DateTime minDateTime = DateTime.MinValue;
            dateTimeExpirationSub = new DTODateTime(minDateTime.Year, minDateTime.Month, minDateTime.Day);
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

    [Serializable]
    public struct DTODateTime
    {
        public int year;
        public int month;
        public int day;

        public DTODateTime(int year, int month, int day)
        {
            this.year = year;
            this.month = month;
            this.day = day;
        }


        public void SetNewDateTime(DateTime dateTime)
        {
            this.year = dateTime.Year;
            this.month = dateTime.Month;
            this.day = dateTime.Day;
        }

        public DateTime ToDateTime()
        {
            return new DateTime(year, month, day);
        }
    }
}