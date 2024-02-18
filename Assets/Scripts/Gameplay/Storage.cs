using Services;
using Services.SaveLoad;

namespace Gameplay
{
    public class Storage : IService
    {
        private int _money;

        public Storage()
        {
            _money = AllServices.Container.Get<ISaveLoadService>().DataProgress.money;
        }

        public bool TryBuy(int price)
        {
            if (_money >= price)
            {
                _money -= price;
                AllServices.Container.Get<EventBus>().ChangeMoney(_money);
                return true;
            }

            return false;
        }
    }
}