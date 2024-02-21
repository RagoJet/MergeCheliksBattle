using System;
using Cysharp.Threading.Tasks;
using Gameplay;
using Gameplay.BeforeTheBattle;
using Gameplay.Cells;
using Services;
using Services.Factories;
using UnityEngine.SceneManagement;

namespace Operations.SceneLoadingOperations
{
    public class GameSceneLoadingOperation : ILoadingOperation
    {
        public string Description => "Loading game scene...";

        public async UniTask Load(Action<float> onProgress)
        {
            onProgress.Invoke(0.1f);
            await SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(Constants.Scenes.MAIN_MENU));
            onProgress.Invoke(0.55f);
            await SceneManager.LoadSceneAsync(Constants.Scenes.GAME, LoadSceneMode.Additive);
            onProgress.Invoke(0.9f);

            SceneManager.SetActiveScene(SceneManager.GetSceneByName(Constants.Scenes.GAME));
            IGameFactory gameFactory = AllServices.Container.Get<IGameFactory>();
            CellGrid cellGrid = gameFactory.CreateCellGrid();
            CreatureMaster creatureMaster = gameFactory.CreateCreatureMaster();
            creatureMaster.Construct(cellGrid);
            gameFactory.CreatePrepareForBattleMenu().Construct(cellGrid, creatureMaster);
            gameFactory.CreateInfoPanel();
        }
    }
}