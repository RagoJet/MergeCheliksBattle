using System;
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
    }
}