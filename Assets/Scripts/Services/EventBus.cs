using System;

namespace Services
{
    public class EventBus : IService
    {
        public event Action onCreatedEnemyCrowd;
        public event Action onDeathCreatureCrowd;
        public event Action onDeathEnemyCrowd;
        public event Action onAllDeadEnemies;
        public event Action onChangeMoney;
        public event Action<int> onDeathEnemyUnit;


        public void OnChangeMoney()
        {
            onChangeMoney?.Invoke();
        }

        public void OnDeathEnemyUnit(int value)
        {
            onDeathEnemyUnit?.Invoke(value);
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
    }
}