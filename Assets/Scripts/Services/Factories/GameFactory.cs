using Gameplay;
using Gameplay.BeforeTheBattle;
using Gameplay.Cells;
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
                _assetProvider.GetAsset<CreatureDescriptions>(Constants.AssetPaths.CREATURE_DESCRIPTIONS);
            _enemyDescriptions =
                _assetProvider.GetAsset<EnemyDescriptions>(Constants.AssetPaths.ENEMY_DESCRIPTIONS);
        }

        public Enemy CreateHuman(int level, Vector3 pos)
        {
            UnitData data = _enemyDescriptions.GetHumanData(level);
            return CreateEnemy(data, pos);
        }

        public Enemy CreateElf(int level, Vector3 pos)
        {
            UnitData data = _enemyDescriptions.GetElvesData(level);
            return CreateEnemy(data, pos);
        }

        public Enemy CreateUndead(int level, Vector3 pos)
        {
            UnitData data = _enemyDescriptions.GetUndeadData(level);
            return CreateEnemy(data, pos);
        }

        public Enemy CreateOrc(int level, Vector3 pos)
        {
            UnitData data = _enemyDescriptions.GetOrcData(level);
            return CreateEnemy(data, pos);
        }

        private Enemy CreateEnemy(UnitData data, Vector3 pos)
        {
            Enemy enemy = Object.Instantiate(data.UnitPrefab, pos, Quaternion.identity) as Enemy;
            enemy.SetData(data);
            return enemy;
        }

        public Creature CreateCreature(int level, Cell cell, Transform parent = null)
        {
            UnitData data = _creatureDescriptions.GetCreatureData(level);
            Creature creature = Object.Instantiate(data.UnitPrefab, cell.GetPosition,
                Quaternion.identity, parent) as Creature;
            creature.SetNewCell(cell);
            creature.SetData(data);

            return creature;
        }

        public CrowdOfCreatures CreateCrowdOfCreatures(Vector3 pos)
        {
            return InstantiateObject(
                _assetProvider.GetAsset<CrowdOfCreatures>(Constants.AssetPaths.CROWD_OF_CREATURES), pos,
                Quaternion.identity);
        }

        public CrowdOfEnemies CreateCrowdOfEnemies(Vector3 pos)
        {
            return InstantiateObject(_assetProvider.GetAsset<CrowdOfEnemies>(Constants.AssetPaths.CROWD_OF_ENEMIES),
                pos, Quaternion.identity);
        }

        public SpawnerCrowds CreateSpawnerCrowds(Vector3 pos)
        {
            return InstantiateObject(_assetProvider.GetAsset<SpawnerCrowds>(Constants.AssetPaths.SPAWNER_CROWDS), pos,
                Quaternion.identity);
        }

        public MyJoyStick CreateMyJoystick()
        {
            return InstantiateObject(_assetProvider.GetAsset<MyJoyStick>(Constants.AssetPaths.MY_JOYSTICK));
        }

        public InfoPanel CreateInfoPanel()
        {
            return InstantiateObject(_assetProvider.GetAsset<InfoPanel>(Constants.AssetPaths.INFO_PANEL));
        }

        public PrepareForBattleMenu CreatePrepareForBattleMenu()
        {
            return InstantiateObject(
                _assetProvider.GetAsset<PrepareForBattleMenu>(Constants.AssetPaths.PREPARE_FOR_BATTLE_MENU));
        }

        public CellGrid CreateCellGrid()
        {
            CellGrid cellGrid = _assetProvider.GetAsset<CellGrid>(Constants.AssetPaths.CELL_GRID);
            return InstantiateObject(cellGrid, cellGrid.transform.position, Quaternion.identity);
        }

        public CreatureMaster CreateCreatureMaster()
        {
            return InstantiateObject(_assetProvider.GetAsset<CreatureMaster>(Constants.AssetPaths.CREATURE_MASTER));
        }

        public LoadingScreen CreateLoadingScreen()
        {
            return InstantiateObject(_assetProvider.GetAsset<LoadingScreen>(Constants.AssetPaths.LOADING_SCREEN));
        }

        public Cell CreateCell(Vector3 pos, Transform parent)
        {
            return InstantiateObject(_assetProvider.GetAsset<Cell>(Constants.AssetPaths.CELL), pos,
                Quaternion.identity, parent);
        }


        private T InstantiateObject<T>(T asset, Vector3 pos, Quaternion rotation, Transform parent = null)
            where T : MonoBehaviour
        {
            T obj = Object.Instantiate(asset, pos, rotation, parent);
            return obj;
        }

        private T InstantiateObject<T>(T asset, Transform parent = null) where T : MonoBehaviour
        {
            return InstantiateObject<T>(asset, Vector3.zero, Quaternion.identity, parent);
        }
    }
}