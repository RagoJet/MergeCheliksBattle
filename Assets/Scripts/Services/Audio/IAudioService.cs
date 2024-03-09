namespace Services.Audio
{
    public interface IAudioService : IService
    {
        public bool Muted { get; set; }

        public void PlayMainMenuMusic();
        public void PlayGameplayMusic();
        public void PlayFightSound();
        public void PlayGetGoldFromKillSound();

        public void PlayLoseSound();

        public void PlayWinSound();

        public void PlayMergeSound();

        public void PlayPickCreatureSound();
    }
}