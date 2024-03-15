namespace Services.Ads
{
    public interface IAdsService : IService
    {
        public void Initialize();
        public void ShowInterstitial();
        public void ShowBanner();
        public void RemoveAds();
        public void HideBanner();
    }
}