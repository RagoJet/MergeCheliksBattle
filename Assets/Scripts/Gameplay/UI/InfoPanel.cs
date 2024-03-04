using Services;
using Services.SaveLoad;
using TMPro;
using UnityEngine;

namespace Gameplay.UI
{
    public class InfoPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _moneyText;
        [SerializeField] private TextMeshProUGUI _levelText;

        private Wallet _wallet;

        public void Construct(Wallet wallet)
        {
            _wallet = wallet;
            AllServices.Container.Get<EventBus>().onChangeMoney += UpdateGoldText;
            UpdateGoldText();
            UpdateLevelText();
        }

        private void UpdateGoldText()
        {
            _moneyText.text = $"Gold: {_wallet.Money}";
        }

        private void UpdateLevelText()
        {
            _levelText.text = $"Level : {AllServices.Container.Get<ISaveLoadService>().DataProgress.levelOfGame}";
        }

        private void OnDisable()
        {
            AllServices.Container.Get<EventBus>().onChangeMoney -= UpdateGoldText;
        }
    }
}