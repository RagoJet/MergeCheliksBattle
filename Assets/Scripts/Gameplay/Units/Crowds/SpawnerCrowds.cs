using System.Collections;
using System.Collections.Generic;
using Gameplay.Units.Creatures;
using Services;
using Services.Factories;
using UnityEngine;

namespace Gameplay.Units.Crowds
{
    public class SpawnerCrowds : MonoBehaviour
    {
        [SerializeField] private Transform[] _enemyCrowdsSpawns;

        public void SpawnEnemyCrowds()
        {
            StartCoroutine(SpawningEnemyCrowds());
        }

        public void SpawnCreaturesCrowd(Vector3 pos, List<Creature> list)
        {
            IGameFactory gameFactory = AllServices.Container.Get<IGameFactory>();
            CrowdOfCreatures crowdOfCreatures = gameFactory.CreateCrowdOfCreatures(pos);
            crowdOfCreatures.Construct(list);
        }

        private IEnumerator SpawningEnemyCrowds()
        {
            IGameFactory gameFactory = AllServices.Container.Get<IGameFactory>();
            foreach (var spawn in _enemyCrowdsSpawns)
            {
                gameFactory.CreateCrowdOfEnemies(spawn.position);
                yield return null;
            }
        }
    }
}