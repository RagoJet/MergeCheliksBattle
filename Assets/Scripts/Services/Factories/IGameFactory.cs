using Gameplay;
using Gameplay.Creatures;
using Services.LoadingScreenNS;
using UnityEngine;

namespace Services.Factories
{
    public interface IGameFactory : IService
    {
        public InfoPanel CreateInfoPanel();
        public PrepareForBattleMenu CreatePrepareForBattleMenu();
        public CellGrid CreateCellGrid();
        public CreatureMaster CreateCreatureMaster();
        public Creature CreateCreature(int level, Cell cell, Transform parent = null);
        public LoadingScreen CreateLoadingScreen();
        public Cell CreateCell(Vector3 pos, Transform parent);
    }
}