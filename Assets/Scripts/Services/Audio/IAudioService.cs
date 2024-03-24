namespace Services.Audio
{
    public interface IAudioService : IService
    {
        public bool MutedMusic { get; set; }
        public bool MutedSounds { get; set; }
        public void GameplayMusic();
        public void StartBattleSound();
        public void PressButtonSound();
        public void AttackSound();
        public void GoldSound();
        public void LoseSound();
        public void WinSound();
        public void MergeSound();
        public void PickUpMergeEntitySound();
        public void DownMergeEntitySound();
        public void BuySound();
        public void NoGoldSound();
    }
}