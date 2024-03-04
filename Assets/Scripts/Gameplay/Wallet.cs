using Services;
using Services.SaveLoad;
using UnityEngine;

namespace Gameplay
{
    public class Wallet : MonoBehaviour
    {
        private int _money;

        public int Money => _money;

        private void Awake()
        {
            _money = AllServices.Container.Get<ISaveLoadService>().DataProgress.money;
            AllServices.Container.Get<EventBus>().onDeathEnemyUnit += AddMoney;
        }


        private void AddMoney(int value)
        {
            if (value > 0)
            {
                _money += value;
                AllServices.Container.Get<ISaveLoadService>().DataProgress.money += value;
                AllServices.Container.Get<EventBus>().OnChangeMoney();
            }
        }

        public bool TryBuy(int price)
        {
            if (_money >= price)
            {
                _money -= price;
                AllServices.Container.Get<ISaveLoadService>().DataProgress.money -= price;
                AllServices.Container.Get<EventBus>().OnChangeMoney();
                return true;
            }

            return false;
        }

        private void OnDestroy()
        {
            AllServices.Container.Get<EventBus>().onDeathEnemyUnit -= AddMoney;
        }
    }
}