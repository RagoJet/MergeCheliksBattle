namespace Services.Audio
{
    public interface IAudioService : IService
    {
        public bool MutedMusic { get; set; }
        public bool MutedClips { get; set; }

        public void PlayGameplayMusic();
        public void PlayAttackSound();
        public void PlayStartBattleSound();
        public void PlayGetGoldFromKillSound();

        public void PlayLoseSound();

        public void PlayWinSound();

        public void PlayMergeSound();

        public void PlayPickUpCreatureSound();
        public void DownCreatureSound();
        public void PlayBuyCreatureSound();
    }
}