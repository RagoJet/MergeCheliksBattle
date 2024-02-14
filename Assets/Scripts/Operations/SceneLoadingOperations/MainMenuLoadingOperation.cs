using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Operations.SceneLoadingOperations
{
    public class MainMenuLoadingOperation : ILoadingOperation
    {
        public string Description => "Loading main menu...";

        public async UniTask Load(Action<float> onProgress)
        {
            onProgress.Invoke(0.7f);
            await SceneManager.LoadSceneAsync(Constants.Scenes.MAIN_MENU, LoadSceneMode.Additive);
        }
    }
}