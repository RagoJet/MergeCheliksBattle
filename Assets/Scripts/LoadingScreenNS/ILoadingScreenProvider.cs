using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Operations;
using Services;

namespace LoadingScreenNS
{
    public interface ILoadingScreenProvider : IService
    {
        public UniTask LoadAndDestroy(ILoadingOperation loadingOperation);
        public UniTask LoadAndDestroy(Queue<ILoadingOperation> loadingOperations);
    }
}