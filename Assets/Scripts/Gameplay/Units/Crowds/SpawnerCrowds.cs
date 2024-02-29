using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Units.Creatures;
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
            StartCoroutine(SpawningEnemyCrowds());
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

        private IEnumerator SpawningEnemyCrowds()
        {
            int levelOfGame = AllServices.Container.Get<ISaveLoadService>().DataProgress.levelOfGame;
            IGameFactory gameFactory = AllServices.Container.Get<IGameFactory>();
            foreach (var spawn in _enemyCrowdsSpawns)
            {
                CrowdOfEnemies crowdOfEnemies = gameFactory.CreateCrowdOfEnemies(spawn.position);
                crowdOfEnemies.Construct(CreateListOfEnemies(levelOfGame, crowdOfEnemies));
                yield return new WaitForSeconds(0.3f);
            }
        }

        private List<Unit> CreateListOfEnemies(int levelOfGame, CrowdOfEnemies crowdOfEnemies)
        {
            IGameFactory gameFactory = AllServices.Container.Get<IGameFactory>();
            List<Unit> enemies = new List<Unit>();
            Func<int, Vector3, Enemy> CreateEnemy = levelOfGame switch
            {
                < 11 => gameFactory.CreateHuman,
                < 21 => gameFactory.CreateElf,
                < 31 => gameFactory.CreateUndead,
                < 41 => gameFactory.CreateOrc,
                _ => throw new ArgumentOutOfRangeException(nameof(levelOfGame), levelOfGame, null)
            };

            AddEnemies(levelOfGame % 10, crowdOfEnemies.transform.position, CreateEnemy);

            void AddEnemies(int levelOfGame, Vector3 pos, Func<int, Vector3, Enemy> CreateEnemy)
            {
                int level = Mathf.Clamp(levelOfGame, 0, 8);
                for (int i = 0; i < 8; i++)
                {
                    int levelOfUnit = Random.Range(0, level);
                    Enemy enemy = CreateEnemy.Invoke(levelOfUnit, pos);
                    enemy.SetCrowd(crowdOfEnemies);
                    enemies.Add(enemy);
                }
            }

            return enemies;
        }
    }
}