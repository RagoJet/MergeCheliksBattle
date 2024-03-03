using Configs;

namespace Services.Factories
{
    public interface IStaticDataFactory : IService
    {
        public GridStaticData GetGridStaticData();
    }
}