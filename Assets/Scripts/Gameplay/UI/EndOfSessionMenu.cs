using Operations.SceneLoadingOperations;
using Services;
using Services.LoadingScreenNS;
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
            ILoadingScreenProvider loadingScreenProvider = AllServices.Container.Get<ILoadingScreenProvider>();
            loadingScreenProvider.LoadAndDestroy(new GameSceneLoadingOperation());
        }
    }
}