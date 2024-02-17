using UnityEngine;

namespace Services.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public T GetAsset<T>(string path) where T : Object
        {
            return Resources.Load<T>(path);
        }
    }
}