using Configs;
using Services.AssetManagement;

namespace Services.Factories
{
    public class StaticDataFactory : IStaticDataFactory
    {
        private IAssetProvider _assetProvider;

        public StaticDataFactory()
        {
            _assetProvider = AllServices.Container.Get<IAssetProvider>();
        }

        public GridStaticData GetGridStaticData()
        {
            return _assetProvider.GetAsset<GridStaticData>(Constants.StaticData.GRID_CELL_DATA);
        }
    }
}