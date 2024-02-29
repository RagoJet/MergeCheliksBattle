using System;
using Cysharp.Threading.Tasks;
using Gameplay.BeforeTheBattle;
using Gameplay.Cells;
using Gameplay.Units.Crowds;
using Services;
using Services.Factories;
using Services.JoySticks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Operations.SceneLoadingOperations
{
    public class GameSceneLoadingOperation : ILoadingOperation
    {
        public string Description => "Loading game scene...";

        public async UniTask Load(Action<float> onProgress)
        {
            onProgress.Invoke(0.9f);
            await SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(Constants.Scenes.MAIN_MENU));
            await SceneManager.LoadSceneAsync(Constants.Scenes.GAME, LoadSceneMode.Additive);
            CreatingObjectsForGame();
        }

        private void CreatingObjectsForGame()
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(Constants.Scenes.GAME));
            IGameFactory gameFactory = AllServices.Container.Get<IGameFactory>();

            MyJoyStick myJoyStick = gameFactory.CreateMyJoystick();
            myJoyStick.SwitchOff();
            AllServices.Container.Register<IJoyStick>(myJoyStick);

            gameFactory.CreateInfoPanel();

            CellGrid cellGrid = gameFactory.CreateCellGrid();
            CreatureMaster creatureMaster = gameFactory.CreateCreatureMaster();
            creatureMaster.Construct(cellGrid);
            SpawnerCrowds spawnerCrowds = gameFactory.CreateSpawnerCrowds(new Vector3(0, 3, 0));
            gameFactory.CreatePrepareForBattleMenu().Construct(cellGrid, creatureMaster, spawnerCrowds);
        }
    }
}