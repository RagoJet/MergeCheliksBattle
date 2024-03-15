namespace Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        public SavedData SavedData { get; }
        void SaveProgress();
        void LoadProgress();
    }
}