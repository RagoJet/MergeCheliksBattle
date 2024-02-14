namespace Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        DataProgress LoadProgress();
    }
}