using Gameplay;
using Gameplay.BeforeTheBattle;
using Gameplay.Cells;
using Gameplay.Units.Creatures;
using Gameplay.Units.Crowds;
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

        public GameFactory()
        {
            _assetProvider = AllServices.Container.Get<IAssetProvider>();
            _creatureDescriptions =
                _assetProvider.GetAsset<CreatureDescriptions>(Constants.AssetPaths.CREATURE_DESCRIPTIONS);
        }


        public Creature CreateCreature(int level, Cell cell, Transform parent = null)
        {
            CreatureDescription creatureDescription = _creatureDescriptions.GetCreatureData(level);
            Creature creature = Object.Instantiate(creatureDescription.CreaturePrefab, cell.GetPosition,
                Quaternion.identity, parent);
            creature.Construct(creatureDescription, cell);

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

        public SpawnerCrowds CreateSpawnerCrowds()
        {
            return InstantiateObject(_assetProvider.GetAsset<SpawnerCrowds>(Constants.AssetPaths.SPAWNER_CROWDS));
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