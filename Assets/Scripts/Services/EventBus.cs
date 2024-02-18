using System;

namespace Services
{
    public class EventBus : IService
    {
        public event Action<int> OnChangeMoney;


        public void ChangeMoney(int money)
        {
            OnChangeMoney.Invoke(money);
        }
    }
}