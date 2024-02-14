using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Factories;
using Operations;
using Services;
using Object = UnityEngine.Object;

namespace LoadingScreenNS
{
    public sealed class LoadingScreenProvider : ILoadingScreenProvider
    {
        public async UniTask LoadAndDestroy(ILoadingOperation loadingOperation)
        {
            var operations = new Queue<ILoadingOperation>();
            operations.Enqueue(loadingOperation);
            await LoadAndDestroy(operations);
        }

        public async UniTask LoadAndDestroy(Queue<ILoadingOperation> loadingOperations)
        {
            var loadingScreen = AllServices.Container.Get<IGameFactory>()
                .Create<LoadingScreen>(Constants.AssetPaths.LoadingScreen);
            await loadingScreen.Load(loadingOperations);
            Object.Destroy(loadingScreen.gameObject);
        }
    }
}