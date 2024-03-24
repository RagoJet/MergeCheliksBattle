using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Gameplay.UI;
using Gameplay.Units.Enemies;
using Services;
using Services.Factories;
using Services.SaveLoad;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Units.Crowds
{
    public class SpawnerCrowds : MonoBehaviour
    {
        [SerializeField] private Transform[] _enemyCrowdsSpawns;


        public void SpawnAllCrowds(Vector3 pos, List<Unit> creatureList)
        {
            CrowdOfMerged crowdOfMerged = SpawnCreaturesCrowd(pos, creatureList);
            SpawningEnemyCrowds(crowdOfMerged.transform);
        }

        private CrowdOfMerged SpawnCreaturesCrowd(Vector3 pos, List<Unit> creatureList)
        {
            IGameFactory gameFactory = AllServices.Container.Get<IGameFactory>();
            CrowdOfMerged crowdOfMerged = gameFactory.CreateCrowdOfCreatures(pos);
            foreach (var unit in creatureList)
            {
                unit.SetCrowd(crowdOfMerged);
            }

            crowdOfMerged.Construct(creatureList);
            crowdOfMerged.GetComponent<InfoOfCrowd>().Init();
            return crowdOfMerged;
        }

        private async UniTask SpawningEnemyCrowds(Transform playerCrowdTransform)
        {
            int levelOfGame = AllServices.Container.Get<ISaveLoadService>().SavedData.levelOfGame;
            IGameFactory gameFactory = AllServices.Container.Get<IGameFactory>();

            EnemyPointersCanvas enemyPointersCanvas = gameFactory.CreateEnemyPointersCanvas();

            foreach (var spawn in _enemyCrowdsSpawns)
            {
                CrowdOfEnemies crowdOfEnemies = gameFactory.CreateCrowdOfEnemies(spawn.position);
                EnemyPointerImage pointerImage = enemyPointersCanvas.CreateEnemyPointer();

                List<Unit> listOfEnemies = await CreateListOfEnemies(levelOfGame, crowdOfEnemies);

                crowdOfEnemies.Construct(listOfEnemies);

                crowdOfEnemies.GetComponent<EnemyCrowdPointer>()
                    .Construct(playerCrowdTransform, pointerImage.transform);

                AllServices.Container.Get<EventBus>().OnCreatedEnemyCrowd();

                crowdOfEnemies.GetComponent<InfoOfCrowd>().Init();
                await UniTask.Delay(70);
            }
        }

        private async UniTask<List<Unit>> CreateListOfEnemies(int levelOfGame, CrowdOfEnemies crowdOfEnemies)
        {
            IGameFactory gameFactory = AllServices.Container.Get<IGameFactory>();
            List<Unit> enemies = new List<Unit>();
            Func<int, Vector3, Unit> CreateEnemy = levelOfGame switch
            {
                < 10 => gameFactory.CreateElf,
                < 20 => gameFactory.CreateUndead,
                < 30 => gameFactory.CreateOrc,
                _ => gameFactory.CreateOrc
            };

            int level = Mathf.Clamp(levelOfGame % 10, 0, 8);
            for (int i = 0; i < 8; i++)
            {
                int levelOfUnit = Random.Range(0, level);
                Unit enemy = CreateEnemy.Invoke(levelOfUnit, crowdOfEnemies.transform.position);
                enemy.SetCrowd(crowdOfEnemies);
                enemies.Add(enemy);
                await UniTask.Delay(70);
            }


            return await UniTask.FromResult(enemies);
        }
    }
}