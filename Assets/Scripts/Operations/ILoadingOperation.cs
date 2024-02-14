using System;
using Cysharp.Threading.Tasks;

namespace Operations
{
    public interface ILoadingOperation
    {
        string Description { get; }
        UniTask Load(Action<float> onProgress);
    }
}