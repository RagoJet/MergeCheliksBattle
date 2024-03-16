using Operations.SceneLoadingOperations;
using Services;
using Services.Ads;
using Services.Audio;
using Services.LoadingScreenNS;
using Services.SaveLoad;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public abstract class EndOfSessionMenu : MonoBehaviour
    {
        [SerializeField] private Button _startLevelButton;

        private void Awake()
        {
            _startLevelButton.onClick.AddListener(StartLevel);
        }

        private void StartLevel()
        {
            AllServices.Container.Get<IAdsService>().ShowInterstitial();
            AllServices.Container.Get<IAudioService>().PressButtonSound();
            AllServices.Container.Get<ISaveLoadService>().SaveProgress();
            ILoadingScreenProvider loadingScreenProvider = AllServices.Container.Get<ILoadingScreenProvider>();
            loadingScreenProvider.LoadAndDestroy(new GameSceneLoadingOperation());
        }
    }
}