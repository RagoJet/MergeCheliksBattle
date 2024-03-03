using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
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

        public void SpawnEnemyCrowds()
        {
            SpawningEnemyCrowds();
        }

        public void SpawnCreaturesCrowd(Vector3 pos, List<Unit> list)
        {
            IGameFactory gameFactory = AllServices.Container.Get<IGameFactory>();
            CrowdOfCreatures crowdOfCreatures = gameFactory.CreateCrowdOfCreatures(pos);
            foreach (var unit in list)
            {
                unit.SetCrowd(crowdOfCreatures);
            }

            crowdOfCreatures.Construct(list);
        }

        private async UniTask SpawningEnemyCrowds()
        {
            int levelOfGame = AllServices.Container.Get<ISaveLoadService>().DataProgress.levelOfGame;
            IGameFactory gameFactory = AllServices.Container.Get<IGameFactory>();

            for (int i = 0; i < 1; i++)
            {
                CrowdOfEnemies crowdOfEnemies = gameFactory.CreateCrowdOfEnemies(_enemyCrowdsSpawns[i].position);
                List<Unit> listOfEnemies = await CreateListOfEnemies(levelOfGame, crowdOfEnemies);
                crowdOfEnemies.Construct(listOfEnemies);
                AllServices.Container.Get<EventBus>().OnCreatedEnemyCrowd();
                await UniTask.Delay(70);
            }

            // foreach (var spawn in _enemyCrowdsSpawns)
            // {
            //     CrowdOfEnemies crowdOfEnemies = gameFactory.CreateCrowdOfEnemies(spawn.position);
            //     List<Unit> listOfEnemies = await CreateListOfEnemies(levelOfGame, crowdOfEnemies);
            //     crowdOfEnemies.Construct(listOfEnemies);
            //     AllServices.Container.Get<EventBus>().OnCreatedEnemyCrowd();
            //     await UniTask.Delay(70);
            // }
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