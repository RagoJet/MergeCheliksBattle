using System;
using Gameplay.UI;
using Services.Factories;
using Services.SaveLoad;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

namespace Services.Ads.AdsScripts
{
    public class IAPButtonListener : MonoBehaviour
    {
        private const string subId = "com.onimka.removeads";

        public void OnPurchaseComplete(Product product)
        {
            Debug.Log(product.definition.id);
            if (product.definition.id != subId)
            {
                ShowPopup(false);
                return;
            }

            if (IsSubscribedTo(product, out DateTime datetime))
            {
                AllServices.Container.Get<IAdsService>().RemoveAds();

                AllServices.Container.Get<ISaveLoadService>().SavedData.dateTimeExpirationSub = datetime;
                AllServices.Container.Get<ISaveLoadService>().SaveProgress();
                ShowPopup(true);
                Destroy(gameObject);
            }
            else
            {
                ShowPopup(false);
            }
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureDescription pfDescription)
        {
            ShowPopup(false);
        }

        public void OnProductFetched(Product product)
        {
        }

        private void ShowPopup(bool success)
        {
            IGameFactory gameFactory = AllServices.Container.Get<IGameFactory>();
            UIPopUp uiPopUp = gameFactory.GetUiPopupAsync();
            // UiPopup uiPopup = await _gameFactory.InstantiateAsync<UiPopup>(Constants.Assets.UI_POPUP);
            uiPopUp.Show(success ? "Ads Removed!" : "Smth went wrong Try again later");
        }

        bool IsSubscribedTo(Product subscription, out DateTime subExpireDate)
        {
            // If the product doesn't have a receipt, then it wasn't purchased and the user is therefore not subscribed.
            if (subscription.receipt == null)
            {
                subExpireDate = default;
                return false;
            }

            //The intro_json parameter is optional and is only used for the App Store to get introductory information.
            var subscriptionManager = new SubscriptionManager(subscription, null);

            // The SubscriptionInfo contains all of the information about the subscription.
            // Find out more: https://docs.unity3d.com/Packages/com.unity.purchasing@3.1/manual/UnityIAPSubscriptionProducts.html
            var info = subscriptionManager.getSubscriptionInfo();

            subExpireDate = subscriptionManager.getSubscriptionInfo().getExpireDate();
            return info.isSubscribed() == Result.True;
        }
    }
}