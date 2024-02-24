using System;
using Cysharp.Threading.Tasks;
using Services;
using Services.SaveLoad;

namespace Operations
{
    public class LoadingDataProgressOperation : ILoadingOperation
    {
        public string Description => "Loading data progress...";

        public async UniTask Load(Action<float> onProgress)
        {
            onProgress.Invoke(0.9f);
            ISaveLoadService saveLoadService = AllServices.Container.Get<ISaveLoadService>();
            saveLoadService.LoadProgress();
            await UniTask.CompletedTask;
        }
    }
}