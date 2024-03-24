using Configs;
using Services.Factories;
using Unity.VisualScripting;
using UnityEngine;

namespace Services.Audio
{
    public class AudioService : IAudioService
    {
        private SoundsData _soundsData;
        private AudioSource _musicAudioSource;
        private AudioSource _clipsAudioSource;

        public AudioService()
        {
            _soundsData = AllServices.Container.Get<IStaticDataFactory>().GetSoundsData();

            _clipsAudioSource = new GameObject("ClipAudioSource").AddComponent<AudioSource>();
            _musicAudioSource = new GameObject("MusicAudioSource").AddComponent<AudioSource>();
            _musicAudioSource.AddComponent<AudioListener>();
            _musicAudioSource.priority = 0;
            _clipsAudioSource.priority = 200;
            _clipsAudioSource.volume = 0.55f;
            _musicAudioSource.loop = true;
            Object.DontDestroyOnLoad(_musicAudioSource);
            Object.DontDestroyOnLoad(_clipsAudioSource);
        }

        public bool MutedMusic
        {
            get => _musicAudioSource.mute;
            set => _musicAudioSource.mute = value;
        }

        public bool MutedSounds
        {
            get => _clipsAudioSource.mute;
            set => _clipsAudioSource.mute = value;
        }

        public void GameplayMusic()
        {
            _musicAudioSource.Stop();
            _musicAudioSource.clip = _soundsData.gameplayMusics[Random.Range(0, _soundsData.gameplayMusics.Length)];
            _musicAudioSource.Play();
        }

        public void StartBattleSound()
        {
            _musicAudioSource.PlayOneShot(_soundsData.startBattleClip);
        }


        public void LoseSound()
        {
            _musicAudioSource.Stop();
            _musicAudioSource.PlayOneShot(_soundsData.loseClip);
        }

        public void WinSound()
        {
            _musicAudioSource.Stop();
            _musicAudioSource.PlayOneShot(_soundsData.winClip);
        }

        public void PressButtonSound()
        {
            _clipsAudioSource.PlayOneShot(_soundsData.pressButtonClip);
        }

        public void AttackSound()
        {
            _clipsAudioSource.PlayOneShot(_soundsData.attackClip);
        }


        public void MergeSound()
        {
            _clipsAudioSource.PlayOneShot(_soundsData.mergeClip);
        }

        public void PickUpMergeEntitySound()
        {
            _clipsAudioSource.PlayOneShot(_soundsData.pickUpMergeEntityClip);
        }

        public void DownMergeEntitySound()
        {
            _clipsAudioSource.PlayOneShot(_soundsData.downMergeEntityClip);
        }

        public void BuySound()
        {
            _clipsAudioSource.PlayOneShot(_soundsData.buyClip);
        }

        public void NoGoldSound()
        {
            _clipsAudioSource.PlayOneShot(_soundsData.noGoldClip);
        }

        public void GoldSound()
        {
            _clipsAudioSource.PlayOneShot(_soundsData.getGoldClip);
        }
    }
}