using Services;
using Services.SaveLoad;
using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class InfoPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _moneyText;

        private void Awake()
        {
            AllServices.Container.Get<EventBus>().OnChangeMoney += UpdateGoldText;
            UpdateGoldText(AllServices.Container.Get<ISaveLoadService>().DataProgress.money);
        }

        private void UpdateGoldText(int money)
        {
            _moneyText.text = $"Gold: {money}";
        }
    }
}