using Gameplay;
using Gameplay.BeforeTheBattle;
using Gameplay.Cells;
using Gameplay.Creatures;
using Gameplay.Crowds;
using Services.JoySticks;
using Services.LoadingScreenNS;
using UnityEngine;

namespace Services.Factories
{
    public interface IGameFactory : IService
    {
        public MyJoyStick CreateMyJoystick();
        public InfoPanel CreateInfoPanel();
        public PrepareForBattleMenu CreatePrepareForBattleMenu();
        public CrowdOfCreatures CreateCrowdOfCreatures(Vector3 pos);
        public CrowdOfEnemies CreateCrowdOfEnemies(Vector3 pos);
        public SpawnerCrowds CreateSpawnerCrowds();
        public CellGrid CreateCellGrid();
        public CreatureMaster CreateCreatureMaster();
        public Creature CreateCreature(int level, Cell cell, Transform parent = null);
        public LoadingScreen CreateLoadingScreen();
        public Cell CreateCell(Vector3 pos, Transform parent);
    }
}