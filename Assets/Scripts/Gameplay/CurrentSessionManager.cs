using Services;
using Services.Factories;
using Services.SaveLoad;
using UnityEngine;
using EventBus = Services.EventBus;

namespace Gameplay
{
    public class CurrentSessionManager : MonoBehaviour
    {
        private int _currentAliveEnemySpawns;

        private void OnEnable()
        {
            AllServices.Container.Get<EventBus>().onCreatedEnemyCrowd += AddCountEnemySpawns;
            AllServices.Container.Get<EventBus>().onDeathEnemyCrowd += RemoveCountEnemySpawns;

            AllServices.Container.Get<EventBus>().onAllDeadEnemies += OpenWinMenu;
            AllServices.Container.Get<EventBus>().onDeathCreatureCrowd += OpenLoseMenu;
        }

        private void AddCountEnemySpawns()
        {
            _currentAliveEnemySpawns++;
        }

        private void RemoveCountEnemySpawns()
        {
            _currentAliveEnemySpawns--;
            if (_currentAliveEnemySpawns == 0)
            {
                AllServices.Container.Get<ISaveLoadService>().DataProgress.levelOfGame++;
                AllServices.Container.Get<EventBus>().OnAllDeadEnemies();
                Destroy(gameObject);
            }
        }

        private void OpenLoseMenu()
        {
            AllServices.Container.Get<IGameFactory>().CreateLoseMenu();
        }

        private void OpenWinMenu()
        {
            AllServices.Container.Get<IGameFactory>().CreateWinMenu();
        }

        private void OnDisable()
        {
            AllServices.Container.Get<EventBus>().onCreatedEnemyCrowd -= AddCountEnemySpawns;
            AllServices.Container.Get<EventBus>().onDeathEnemyCrowd -= RemoveCountEnemySpawns;

            AllServices.Container.Get<EventBus>().onAllDeadEnemies -= OpenWinMenu;
            AllServices.Container.Get<EventBus>().onDeathCreatureCrowd -= OpenLoseMenu;
        }
    }
}