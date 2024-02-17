using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Operations;
using Services.Factories;
using Object = UnityEngine.Object;

namespace Services.LoadingScreenNS
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
                .CreateLoadingScreen();
            await loadingScreen.Load(loadingOperations);
            Object.Destroy(loadingScreen.gameObject);
        }
    }
}