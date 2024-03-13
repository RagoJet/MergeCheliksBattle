using Gameplay;
using Gameplay.Cells;
using Gameplay.UI;
using Gameplay.Units;
using Gameplay.Units.Creatures;
using Gameplay.Units.Crowds;
using Gameplay.Units.Enemies;
using Services.AssetManagement;
using Services.JoySticks;
using Services.LoadingScreenNS;
using UnityEngine;

namespace Services.Factories
{
    public class GameFactory : IGameFactory
    {
        private IAssetProvider _assetProvider;
        private CreatureDescriptions _creatureDescriptions;
        private EnemyDescriptions _enemyDescriptions;

        public GameFactory()
        {
            _assetProvider = AllServices.Container.Get<IAssetProvider>();
            _creatureDescriptions =
                _assetProvider.GetAsset<CreatureDescriptions>(Constants.StaticData.CREATURE_DESCRIPTIONS);
            _enemyDescriptions =
                _assetProvider.GetAsset<EnemyDescriptions>(Constants.StaticData.ENEMY_DESCRIPTIONS);
        }

        public Enemy CreateElf(int level, Vector3 pos)
        {
            UnitData data = _enemyDescriptions.GetElvesData(level);
            return CreateUnit(data, pos) as Enemy;
        }

        public Enemy CreateUndead(int level, Vector3 pos)
        {
            UnitData data = _enemyDescriptions.GetUndeadData(level);
            return CreateUnit(data, pos) as Enemy;
        }

        public Enemy CreateOrc(int level, Vector3 pos)
        {
            UnitData data = _enemyDescriptions.GetOrcData(level);
            return CreateUnit(data, pos) as Enemy;
        }

        private Unit CreateUnit(UnitData data, Vector3 pos)
        {
            Unit unit = Object.Instantiate(data.UnitPrefab, pos, Quaternion.identity);
            unit.SetData(data);
            unit.Refresh();
            return unit;
        }

        public Creature CreateCreature(bool isRange, int level, Cell cell, Transform parent = null)
        {
            UnitData data;
            if (isRange)
            {
                data = _creatureDescriptions.GetRangeUnitData(level);
            }
            else
            {
                data = _creatureDescriptions.GetMeleeUnitData(level);
            }

            Creature creature = CreateUnit(data, cell.GetPosition) as Creature;
            creature.SetNewCell(cell);

            return creature;
        }

        public CrowdOfCreatures CreateCrowdOfCreatures(Vector3 pos)
        {
            return Object.Instantiate(
                _assetProvider.GetAsset<CrowdOfCreatures>(Constants.AssetPaths.CROWD_OF_CREATURES), pos,
                Quaternion.identity);
        }

        public CrowdOfEnemies CreateCrowdOfEnemies(Vector3 pos)
        {
            return Object.Instantiate(_assetProvider.GetAsset<CrowdOfEnemies>(Constants.AssetPaths.CROWD_OF_ENEMIES),
                pos, Quaternion.identity);
        }

        public SpawnerCrowds CreateSpawnerCrowds(Vector3 pos)
        {
            return Object.Instantiate(_assetProvider.GetAsset<SpawnerCrowds>(Constants.AssetPaths.SPAWNER_CROWDS), pos,
                Quaternion.identity);
        }

        public CurrentSessionManager CreateSessionManager()
        {
            return Object.Instantiate(
                _assetProvider.GetAsset<CurrentSessionManager>(Constants.AssetPaths.SESSION_MANAGER));
        }

        public Wallet CreateWallet()
        {
            return Object.Instantiate(_assetProvider.GetAsset<Wallet>(Constants.AssetPaths.WALLET));
        }

        public MyJoyStick CreateMyJoystick()
        {
            return Object.Instantiate(_assetProvider.GetAsset<MyJoyStick>(Constants.AssetPaths.MY_JOYSTICK));
        }

        public CellGrid CreateCellGrid()
        {
            CellGrid cellGrid = _assetProvider.GetAsset<CellGrid>(Constants.AssetPaths.CELL_GRID);
            return Object.Instantiate(cellGrid, cellGrid.transform.position, Quaternion.identity);
        }

        public CreatureMaster CreateCreatureMaster()
        {
            return Object.Instantiate(_assetProvider.GetAsset<CreatureMaster>(Constants.AssetPaths.CREATURE_MASTER));
        }


        public Cell CreateCell(Vector3 pos, Transform parent)
        {
            return Object.Instantiate(_assetProvider.GetAsset<Cell>(Constants.AssetPaths.CELL), pos,
                Quaternion.identity, parent);
        }

        public LoadingScreen CreateLoadingScreen()
        {
            return Object.Instantiate(_assetProvider.GetAsset<LoadingScreen>(Constants.AssetPaths.LOADING_SCREEN));
        }

        public EnemyPointerImage CreateEnemyPointerImage(Transform parent)
        {
            return Object.Instantiate(
                _assetProvider.GetAsset<EnemyPointerImage>(Constants.AssetPaths.ENEMY_POINTER_IMAGE), parent);
        }

        public EnemyPointersCanvas CreateEnemyPointersCanvas()
        {
            return Object.Instantiate(
                _assetProvider.GetAsset<EnemyPointersCanvas>(Constants.AssetPaths.ENEMY_POINTERS_CANVAS));
        }

        public LoseMenu CreateLoseMenu()
        {
            return Object.Instantiate(_assetProvider.GetAsset<LoseMenu>(Constants.AssetPaths.LOSE_MENU));
        }

        public SettingsWindow CreateSettingsWindow()
        {
            return Object.Instantiate(_assetProvider.GetAsset<SettingsWindow>(Constants.AssetPaths.SETTINGS_WINDOW));
        }

        public WinMenu CreateWinMenu()
        {
            return Object.Instantiate(_assetProvider.GetAsset<WinMenu>(Constants.AssetPaths.WIN_MENU));
        }

        public PrepareForBattleMenu CreatePrepareForBattleMenu()
        {
            return Object.Instantiate(
                _assetProvider.GetAsset<PrepareForBattleMenu>(Constants.AssetPaths.PREPARE_FOR_BATTLE_MENU));
        }

        public InfoPanel CreateInfoPanel()
        {
            return Object.Instantiate(_assetProvider.GetAsset<InfoPanel>(Constants.AssetPaths.INFO_PANEL));
        }
    }
}