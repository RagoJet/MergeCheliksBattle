using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Operations.SceneLoadingOperations
{
    public class GameSceneLoadingOperation : ILoadingOperation
    {
        public string Description => "Loading game scene...";

        public async UniTask Load(Action<float> onProgress)
        {
            onProgress.Invoke(0.4f);
            await SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(Constants.Scenes.MAIN_MENU));
            onProgress.Invoke(0.9f);
            await SceneManager.LoadSceneAsync(Constants.Scenes.GAME, LoadSceneMode.Additive);
        }
    }
}