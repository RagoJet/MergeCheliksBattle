using System;
using Gameplay.Creatures;

namespace Services
{
    public class EventBus : IService
    {
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
    }
}