using Services;
using Services.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class SettingsWindow : MonoBehaviour
    {
        [SerializeField] Button _closeButton;
        [SerializeField] Button _muteMusicButton;
        [SerializeField] Button _muteSoundsButton;

        [SerializeField] private Sprite _musicIcon;
        [SerializeField] private Sprite _noMusicIcon;
        [SerializeField] private Sprite _soundIcon;
        [SerializeField] private Sprite _noSoundIcon;


        private void Awake()
        {
            _closeButton.onClick.AddListener(CloseWindow);
            _muteMusicButton.onClick.AddListener(SwitchMusic);
            _muteSoundsButton.onClick.AddListener(SwitchSound);
            AllServices.Container.Get<EventBus>().OnOpenedUIWindow();
        }

        private void OnEnable()
        {
            IAudioService audioService = AllServices.Container.Get<IAudioService>();
            if (audioService.MutedSounds)
            {
                _muteSoundsButton.image.sprite = _noSoundIcon;
            } else
            {
                _muteSoundsButton.image.sprite = _soundIcon;
            }

            if (audioService.MutedMusic)
            {
                _muteMusicButton.image.sprite = _noMusicIcon;
            } else
            {
                _muteMusicButton.image.sprite = _musicIcon;
            }
        }

        private void SwitchSound()
        {
            IAudioService audioService = AllServices.Container.Get<IAudioService>();
            if (audioService.MutedSounds)
            {
                audioService.MutedSounds = false;
                _muteSoundsButton.image.sprite = _soundIcon;
                AllServices.Container.Get<IAudioService>().PressButtonSound();
            } else
            {
                audioService.MutedSounds = true;
                _muteSoundsButton.image.sprite = _noSoundIcon;
            }
        }

        private void SwitchMusic()
        {
            AllServices.Container.Get<IAudioService>().PressButtonSound();
            IAudioService audioService = AllServices.Container.Get<IAudioService>();
            if (audioService.MutedMusic)
            {
                audioService.MutedMusic = false;
                _muteMusicButton.image.sprite = _musicIcon;
            } else
            {
                audioService.MutedMusic = true;
                _muteMusicButton.image.sprite = _noMusicIcon;
            }
        }

        private void CloseWindow()
        {
            AllServices.Container.Get<IAudioService>().PressButtonSound();
            AllServices.Container.Get<EventBus>().OnClosedUIWindow();
            Destroy(gameObject);
        }
    }
}