using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Operations;

namespace Services.LoadingScreenNS
{
    public interface ILoadingScreenProvider : IService
    {
        public UniTask LoadAndDestroy(ILoadingOperation loadingOperation);
        public UniTask LoadAndDestroy(Queue<ILoadingOperation> loadingOperations);
    }
}