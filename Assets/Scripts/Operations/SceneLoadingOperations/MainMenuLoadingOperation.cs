using System;
using Cysharp.Threading.Tasks;
using Services;
using Services.Audio;
using UnityEngine.SceneManagement;

namespace Operations.SceneLoadingOperations
{
    public class MainMenuLoadingOperation : ILoadingOperation
    {
        public string Description => "Loading main menu...";

        public async UniTask Load(Action<float> onProgress)
        {
            onProgress.Invoke(0.2f);
            Scene gameScene = SceneManager.GetSceneByName(Constants.Scenes.GAME);
            if (gameScene.IsValid())
            {
                await SceneManager.UnloadSceneAsync(gameScene);
            }

            AllServices.Container.Get<IAudioService>().PlayMainMenuMusic();

            onProgress.Invoke(0.8f);
            await SceneManager.LoadSceneAsync(Constants.Scenes.MAIN_MENU, LoadSceneMode.Additive);
        }
    }
}