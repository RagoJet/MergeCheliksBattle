using Operations.SceneLoadingOperations;
using Services;
using Services.LoadingScreenNS;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.MenuUI
{
    public abstract class EndOfSessionMenu : MonoBehaviour
    {
        [SerializeField] private Button _startLevelButton;
        [SerializeField] private Button _exitButtonToMenu;

        private void Awake()
        {
            _startLevelButton.onClick.AddListener(StartLevel);
            _exitButtonToMenu.onClick.AddListener(ExitToMenu);
        }

        private void StartLevel()
        {
            ILoadingScreenProvider loadingScreenProvider = AllServices.Container.Get<ILoadingScreenProvider>();
            loadingScreenProvider.LoadAndDestroy(new GameSceneLoadingOperation());
        }

        private void ExitToMenu()
        {
            ILoadingScreenProvider loadingScreenProvider = AllServices.Container.Get<ILoadingScreenProvider>();
            loadingScreenProvider.LoadAndDestroy(new MainMenuLoadingOperation());
        }
    }
}