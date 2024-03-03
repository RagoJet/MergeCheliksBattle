using System;

namespace Services
{
    public class EventBus : IService
    {
        public event Action onCreatedEnemyCrowd;
        public event Action onKilledEnemyCrowd;
        public event Action onDeathCreatureCrowd;
        public event Action onAllDeadEnemies;
        public event Action onChangeMoney;


        public void OnChangeMoney()
        {
            onChangeMoney?.Invoke();
        }

        public void OnAllDeadEnemies()
        {
            onAllDeadEnemies?.Invoke();
        }

        public void OnDeathCreatureCrowd()
        {
            onDeathCreatureCrowd?.Invoke();
        }

        public void OnKilledEnemyCrowd()
        {
            onKilledEnemyCrowd?.Invoke();
        }

        public void OnCreatedEnemyCrowd()
        {
            onCreatedEnemyCrowd?.Invoke();
        }
    }
}