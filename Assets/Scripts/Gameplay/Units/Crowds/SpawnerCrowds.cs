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
            CrowdOfCreatures crowdOfCreatures = SpawnCreaturesCrowd(pos, creatureList);
            SpawningEnemyCrowds(crowdOfCreatures.transform);
        }

        private CrowdOfCreatures SpawnCreaturesCrowd(Vector3 pos, List<Unit> creatureList)
        {
            IGameFactory gameFactory = AllServices.Container.Get<IGameFactory>();
            CrowdOfCreatures crowdOfCreatures = gameFactory.CreateCrowdOfCreatures(pos);
            foreach (var unit in creatureList)
            {
                unit.SetCrowd(crowdOfCreatures);
            }

            crowdOfCreatures.Construct(creatureList);
            crowdOfCreatures.GetComponent<InfoOfCrowd>().Init();
            return crowdOfCreatures;
        }

        private async UniTask SpawningEnemyCrowds(Transform playerCrowdTransform)
        {
            int levelOfGame = AllServices.Container.Get<ISaveLoadService>().DataProgress.levelOfGame;
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
            Func<int, Vector3, Enemy> CreateEnemy = levelOfGame switch
            {
                < 10 => gameFactory.CreateHuman,
                < 20 => gameFactory.CreateElf,
                < 30 => gameFactory.CreateUndead,
                < 40 => gameFactory.CreateOrc,
                _ => throw new ArgumentOutOfRangeException(nameof(levelOfGame), levelOfGame, null)
            };

            int level = Mathf.Clamp(levelOfGame % 10, 0, 8);
            for (int i = 0; i < 8; i++)
            {
                int levelOfUnit = Random.Range(0, level);
                Enemy enemy = CreateEnemy.Invoke(levelOfUnit, crowdOfEnemies.transform.position);
                enemy.SetCrowd(crowdOfEnemies);
                enemies.Add(enemy);
                await UniTask.Delay(70);
            }


            return await UniTask.FromResult(enemies);
        }
    }
}