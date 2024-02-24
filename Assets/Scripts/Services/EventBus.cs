using System;
using Gameplay.Creatures;
using Services.SaveLoad;

namespace Services
{
    public class EventBus : IService
    {
        public event Action OnCompleteLevel;
        public event Action<int> OnChangeMoney;


        public event Action<Creature> OnDeathCreature;


        public void ChangeMoney(int money)
        {
            OnChangeMoney?.Invoke(money);
        }

        public void AfterDeathCreature(Creature creature)
        {
            OnDeathCreature?.Invoke(creature);
        }

        public void CompleteLevel()
        {
            AllServices.Container.Get<ISaveLoadService>().DataProgress.levelOfGame++;
            OnCompleteLevel.Invoke();
        }
    }
}