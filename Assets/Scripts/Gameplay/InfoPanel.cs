using Services;
using Services.SaveLoad;
using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class InfoPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _moneyText;
        [SerializeField] private TextMeshProUGUI _levelText;

        private void OnEnable()
        {
            AllServices.Container.Get<EventBus>().OnChangeMoney += UpdateGoldText;
            AllServices.Container.Get<EventBus>().OnCompleteLevel += UpdateLevelText;
            UpdateGoldText(AllServices.Container.Get<ISaveLoadService>().DataProgress.money);
            UpdateLevelText();
        }

        private void UpdateGoldText(int money)
        {
            _moneyText.text = $"Gold: {money}";
        }

        private void UpdateLevelText()
        {
            _levelText.text = $"Level : {AllServices.Container.Get<ISaveLoadService>().DataProgress.levelOfGame}";
        }

        private void OnDisable()
        {
            AllServices.Container.Get<EventBus>().OnChangeMoney -= UpdateGoldText;
            AllServices.Container.Get<EventBus>().OnCompleteLevel -= UpdateLevelText;
        }
    }
}