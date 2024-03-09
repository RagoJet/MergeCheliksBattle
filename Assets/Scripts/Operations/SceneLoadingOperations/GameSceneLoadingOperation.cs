using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Gameplay;
using Gameplay.Cells;
using Gameplay.Units.Crowds;
using Services;
using Services.Audio;
using Services.Factories;
using Services.JoySticks;
using Services.SaveLoad;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Operations.SceneLoadingOperations
{
    public class GameSceneLoadingOperation : ILoadingOperation
    {
        public string Description => "Loading game scene...";

        public async UniTask Load(Action<float> onProgress)
        {
            onProgress.Invoke(0.1f);

            Scene mainMenuScene = SceneManager.GetSceneByName(Constants.Scenes.MAIN_MENU);
            if (mainMenuScene.IsValid())
            {
                await SceneManager.UnloadSceneAsync(mainMenuScene);
            }

            onProgress.Invoke(0.3f);
            Scene gameScene = SceneManager.GetSceneByName(Constants.Scenes.GAME);
            if (gameScene.IsValid())
            {
                await SceneManager.UnloadSceneAsync(gameScene);
            }

            onProgress.Invoke(0.9f);
            await SceneManager.LoadSceneAsync(Constants.Scenes.GAME, LoadSceneMode.Additive);
            CreatingObjectsForGame();
            AllServices.Container.Get<IAudioService>().PlayGameplayMusic();
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