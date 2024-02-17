using System;
using System.Collections.Generic;
using Operations.SceneLoadingOperations;
using Services;
using Services.LoadingScreenNS;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu
{
    public class UIMainMenu : MonoBehaviour
    {
        [SerializeField] private Button _startNewGameButton;
        [SerializeField] private Button _loadingGameButton;
        [SerializeField] private Button _exitButton;

        private void Awake()
        {
            _startNewGameButton.onClick.AddListener(StartNewGame);
            _loadingGameButton.onClick.AddListener(LoadingGame);
            _exitButton.onClick.AddListener(Exit);
        }

        private void StartNewGame()
        {
            ILoadingScreenProvider loadingScreenProvider = AllServices.Container.Get<ILoadingScreenProvider>();
            loadingScreenProvider.LoadAndDestroy(new GameSceneLoadingOperation());
        }

        private void LoadingGame()
        {
        }

        private void Exit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}