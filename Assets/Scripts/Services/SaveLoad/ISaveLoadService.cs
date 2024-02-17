namespace Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        public DataProgress DataProgress { get; }
        void SaveProgress();
        void LoadProgress();
    }
}