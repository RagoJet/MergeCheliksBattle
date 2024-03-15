using AppodealStack.Monetization.Api;
using AppodealStack.Monetization.Common;
using UnityEngine;

namespace Services.Ads
{
    public class AdsService : IAdsService
    {
        private bool _active;
        private const string APP_KEY = "57c9d95701c8456bfb1f1ca92e8d028e0270bbd33f1c08cf";

        public void Initialize()
        {
            _active = true;

            int adTypes = AppodealAdType.Interstitial | AppodealAdType.Banner;

            Appodeal.SetAutoCache(adTypes, true);

            AppodealCallbacks.Sdk.OnInitialized += OnAppodealInitialized;

            Appodeal.Initialize(APP_KEY, adTypes);
        }

        public void ShowInterstitial()
        {
            if (!_active)
            {
                return;
            }

            if (Appodeal.IsLoaded(AppodealAdType.Interstitial))
            {
                Appodeal.Show(AppodealAdType.Interstitial);
            }
        }

        public void ShowBanner()
        {
            if (!_active)
            {
                return;
            }

            Appodeal.Show(AppodealShowStyle.BannerBottom);
        }

        public void RemoveAds()
        {
            _active = false;
            HideBanner();
        }

        public void HideBanner()
        {
            Appodeal.Hide(AppodealAdType.Banner);
        }

        private void OnAppodealInitialized(object sender, SdkInitializedEventArgs e)
        {
            _active = true;
            Debug.Log("Appodeal Initialized");
        }
    }
}