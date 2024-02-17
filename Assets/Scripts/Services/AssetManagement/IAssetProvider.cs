using UnityEngine;

namespace Services.AssetManagement
{
    public interface IAssetProvider : IService
    {
        public T GetAsset<T>(string path) where T : Object;
    }
}