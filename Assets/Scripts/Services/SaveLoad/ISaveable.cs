namespace Services.SaveLoad
{
    public interface ISaveable
    {
        public void Load();

        public void Save(DataProgress dataProgress);
    }
}