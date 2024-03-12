using System;
using Cysharp.Threading.Tasks;
using Gameplay;
using Gameplay.Cells;
using Gameplay.Units.Crowds;
using Services;
using Services.Audio;
using Services.Factories;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Operations.SceneLoadingOperations
{
    public class GameSceneLoadingOperation : ILoadingOperation
    {
        public string Description => "Loading game scene...";

        public async UniTask Load(Action<float> onProgress)
        {
            onProgress.Invoke(0.3f);
            Scene gameScene = SceneManager.GetSceneByName(Constants.Scenes.GAME);
            if (gameScene.IsValid())
            {
                await SceneManager.UnloadSceneAsync(gameScene);
            }

            AllServices.Container.Get<IAudioService>().PlayGameplayMusic();
            onProgress.Invoke(0.97f);
            await SceneManager.LoadSceneAsync(Constants.Scenes.GAME, LoadSceneMode.Additive);
            CreatingObjectsForGame();
        }

        private void CreatingObjectsForGame()
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(Constants.Scenes.GAME));
            IGameFactory gameFactory = AllServices.Container.Get<IGameFactory>();


            Wallet wallet = gameFactory.CreateWallet();
            gameFactory.CreateSessionManager();

            CellGrid cellGrid = gameFactory.CreateCellGrid();
            CreatureMaster creatureMaster = gameFactory.CreateCreatureMaster();
            creatureMaster.Construct(cellGrid);
            SpawnerCrowds spawnerCrowds = gameFactory.CreateSpawnerCrowds(new Vector3(0, 3, 0));
            gameFactory.CreatePrepareForBattleMenu().Construct(cellGrid, creatureMaster, spawnerCrowds, wallet);


            gameFactory.CreateInfoPanel().Construct(wallet);
        }
    }
}