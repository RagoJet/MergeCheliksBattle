using System;
using Cysharp.Threading.Tasks;
using Services;
using Services.Ads;
using Services.SaveLoad;

namespace Operations
{
    public class LoadingDataProgressOperation : ILoadingOperation
    {
        public string Description => "Loading saved progress...";

        public async UniTask Load(Action<float> onProgress)
        {
            onProgress.Invoke(0.9f);
            ISaveLoadService saveLoadService = AllServices.Container.Get<ISaveLoadService>();
            saveLoadService.LoadProgress();
            if (saveLoadService.SavedData.dateTimeExpirationSub.ToDateTime().CompareTo(DateTime.Now) < 0)
            {
                AllServices.Container.Get<IAdsService>().Initialize();
            }

            await UniTask.CompletedTask;
        }
    }
}