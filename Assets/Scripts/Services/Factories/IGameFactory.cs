using Gameplay;
using Gameplay.Cells;
using Gameplay.MergeEntities;
using Gameplay.UI;
using Gameplay.Units;
using Gameplay.Units.Crowds;
using Services.JoySticks;
using Services.LoadingScreenNS;
using UnityEngine;

namespace Services.Factories
{
    public interface IGameFactory : IService
    {
        public EnemyPointerImage CreateEnemyPointerImage(Transform parent);
        public EnemyPointersCanvas CreateEnemyPointersCanvas();
        public LoseMenu CreateLoseMenu();
        public SettingsWindow CreateSettingsWindow();
        public WinMenu CreateWinMenu();
        public PrepareForBattleMenu CreatePrepareForBattleMenu();
        public InfoPanel CreateInfoPanel();
        public LoadingScreen CreateLoadingScreen();
        public UIPopUp GetUiPopupAsync();
        public CurrentSessionManager CreateSessionManager();
        public Wallet CreateWallet();
        public MyJoyStick CreateMyJoystick();
        public CrowdOfMerged CreateCrowdOfCreatures(Vector3 pos);
        public CrowdOfEnemies CreateCrowdOfEnemies(Vector3 pos);
        public SpawnerCrowds CreateSpawnerCrowds(Vector3 pos);
        public CellGrid CreateCellGrid();
        public MergeMaster CreateCreatureMaster();
        public MergeEntity CreateMergeEntity(bool isRange, int level, Cell cell, Transform parent = null);
        public Cell CreateCell(Vector3 pos, Transform parent);
        public Unit CreateElf(int level, Vector3 pos);
        public Unit CreateUndead(int level, Vector3 pos);
        public Unit CreateOrc(int level, Vector3 pos);
    }
}