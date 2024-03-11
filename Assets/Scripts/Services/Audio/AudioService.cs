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
            _musicAudioSource = new GameObject("MusicAudioSource").AddComponent<AudioSource>();
            _clipsAudioSource = new GameObject("ClipAudioSource").AddComponent<AudioSource>();
            _musicAudioSource.AddComponent<AudioListener>();
            _musicAudioSource.loop = true;
            Object.DontDestroyOnLoad(_musicAudioSource);
            Object.DontDestroyOnLoad(_clipsAudioSource);
        }

        public bool MutedMusic
        {
            get => _musicAudioSource.mute;
            set => _musicAudioSource.mute = value;
        }
        
        public bool MutedClips
        {
            get => _clipsAudioSource.mute;
            set => _clipsAudioSource.mute = value;
        }

        public void PlayGameplayMusic()
        {
            _musicAudioSource.Stop();
            _musicAudioSource.clip = _soundsData.gameplayMusics[Random.Range(0, _soundsData.gameplayMusics.Length)];
            _musicAudioSource.Play();
        }

        public void PlayAttackSound()
        {
            _clipsAudioSource.PlayOneShot(_soundsData.attackClip);
        }

        public void PlayStartBattleSound()
        {
            _clipsAudioSource.PlayOneShot(_soundsData.startBattleClip);
        }

        public void PlayBuyCreatureSound()
        {
            _clipsAudioSource.PlayOneShot(_soundsData.buyCreatureClip);
        }

        public void PlayLoseSound()
        {
            _musicAudioSource.PlayOneShot(_soundsData.loseClip);
        }

        public void PlayWinSound()
        {
            _musicAudioSource.PlayOneShot(_soundsData.winClip);
        }

        public void PlayMergeSound()
        {
            _clipsAudioSource.PlayOneShot(_soundsData.mergeClip);
        }

        public void PlayPickUpCreatureSound()
        {
            _clipsAudioSource.PlayOneShot(_soundsData.pickUpCreatureClip);
        }

        public void DownCreatureSound()
        {
            _clipsAudioSource.PlayOneShot(_soundsData.downCreatureClip);
        }

        public void PlayGetGoldFromKillSound()
        {
            _clipsAudioSource.PlayOneShot(_soundsData.enemyKilledClip);
        }
    }
}