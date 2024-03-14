using Services;
using Services.Audio;
using Services.SaveLoad;
using UnityEngine;

namespace Gameplay
{
    public class Wallet : MonoBehaviour
    {
        private int _gold;

        public int Gold => _gold;

        private void Awake()
        {
            _gold = AllServices.Container.Get<ISaveLoadService>().DataProgress.gold;
            AllServices.Container.Get<EventBus>().onEnemyUnitDeath += AddMoney;
        }


        private void AddMoney(int value)
        {
            _gold += value;
            AllServices.Container.Get<ISaveLoadService>().DataProgress.gold += value;
        }

        public bool TryBuy(int price)
        {
            if (_gold >= price)
            {
                AllServices.Container.Get<IAudioService>().BuySound();
                _gold -= price;
                AllServices.Container.Get<ISaveLoadService>().DataProgress.gold -= price;
                AllServices.Container.Get<EventBus>().OnBuy();
                return true;
            }
            else
            {
                AllServices.Container.Get<IAudioService>().NoGoldSound();
                return false;
            }
        }

        private void OnDestroy()
        {
            AllServices.Container.Get<EventBus>().onEnemyUnitDeath -= AddMoney;
        }
    }
}