using Gameplay;
using Gameplay.BeforeTheBattle;
using Gameplay.Cells;
using Gameplay.Units.Creatures;
using Gameplay.Units.Crowds;
using Gameplay.Units.Enemies;
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
        public SpawnerCrowds CreateSpawnerCrowds(Vector3 pos);
        public CellGrid CreateCellGrid();
        public CreatureMaster CreateCreatureMaster();
        public Creature CreateCreature(int level, Cell cell, Transform parent = null);
        public LoadingScreen CreateLoadingScreen();
        public Cell CreateCell(Vector3 pos, Transform parent);
        public Enemy CreateHuman(int level, Vector3 pos);
        public Enemy CreateElf(int level, Vector3 pos);
        public Enemy CreateUndead(int level, Vector3 pos);
        public Enemy CreateOrc(int level, Vector3 pos);
    }
}