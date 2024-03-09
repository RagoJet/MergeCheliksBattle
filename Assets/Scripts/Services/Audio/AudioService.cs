using Configs;
using Services.Factories;
using Unity.VisualScripting;
using UnityEngine;

namespace Services.Audio
{
    public class AudioService : IAudioService
    {
        private SoundsData _soundsData;
        private AudioSource _audioSource;

        public AudioService()
        {
            _soundsData = AllServices.Container.Get<IStaticDataFactory>().GetSoundsData();
            _audioSource = new GameObject("AudioSource").AddComponent<AudioSource>();
            _audioSource.AddComponent<AudioListener>();
            _audioSource.loop = true;
            Object.DontDestroyOnLoad(_audioSource);
        }

        public bool Muted
        {
            get => _audioSource.mute;
            set => _audioSource.mute = value;
        }

        public void PlayMainMenuMusic()
        {
            _audioSource.Stop();
            _audioSource.clip = _soundsData.mainMenuMusic;
            _audioSource.Play();
        }

        public void PlayGameplayMusic()
        {
            _audioSource.Stop();
            _audioSource.clip = _soundsData.gameplayMusics[Random.Range(0, _soundsData.gameplayMusics.Length)];
            _audioSource.Play();
        }

        public void PlayFightSound()
        {
            _audioSource.PlayOneShot(_soundsData.startFightClip);
        }

        public void PlayLoseSound()
        {
            _audioSource.Stop();
            _audioSource.PlayOneShot(_soundsData.loseClip);
        }

        public void PlayWinSound()
        {
            _audioSource.Stop();
            _audioSource.PlayOneShot(_soundsData.winClip);
        }

        public void PlayMergeSound()
        {
            _audioSource.PlayOneShot(_soundsData.mergeClip);
        }

        public void PlayPickCreatureSound()
        {
            _audioSource.PlayOneShot(_soundsData.pickUpCreatureClip);
        }

        public void PlayGetGoldFromKillSound()
        {
            _audioSource.PlayOneShot(_soundsData.enemyKilledClip);
        }
    }
}