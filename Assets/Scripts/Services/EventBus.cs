using System;
using UnityEngine;

namespace Services
{
    public class EventBus : IService
    {
        public event Action onCreatedEnemyCrowd;
        public event Action onDeathCreatureCrowd;
        public event Action onDeathEnemyCrowd;
        public event Action onAllDeadEnemies;
        public event Action onBuy;
        public event Action<int> onEnemyUnitDeath;

        public event Action<Transform, int> onGetGoldFrom;


        public event Action onOpenedUIWindow;
        public event Action onClosedUIWindow;


        public void OnBuy()
        {
            onBuy?.Invoke();
        }

        public void OnDeathEnemy(Transform transform, int value)
        {
            onEnemyUnitDeath?.Invoke(value);
            onGetGoldFrom?.Invoke(transform, value);
        }

        public void OnAllDeadEnemies()
        {
            onAllDeadEnemies?.Invoke();
        }

        public void OnDeathCreatureCrowd()
        {
            onDeathCreatureCrowd?.Invoke();
        }

        public void OnDeathEnemyCrowd()
        {
            onDeathEnemyCrowd?.Invoke();
        }

        public void OnCreatedEnemyCrowd()
        {
            onCreatedEnemyCrowd?.Invoke();
        }

        public void OnOpenedUIWindow()
        {
            onOpenedUIWindow?.Invoke();
        }

        public void OnClosedUIWindow()
        {
            onClosedUIWindow?.Invoke();
        }
    }
}