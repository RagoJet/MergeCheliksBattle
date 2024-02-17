using System.Collections.Generic;
using Operations;
using Operations.SceneLoadingOperations;
using Services;
using Services.AssetManagement;
using Services.Factories;
using Services.LoadingScreenNS;
using Services.SaveLoad;
using UnityEngine;

public class StartUp : MonoBehaviour
{
    private void Awake()
    {
        RegisterOperation();
        Queue<ILoadingOperation> operationQueue = new Queue<ILoadingOperation>();
        operationQueue.Enqueue(new LoadingDataProgressOperation());
        operationQueue.Enqueue(new MainMenuLoadingOperation());

        ILoadingScreenProvider loadingScreenProvider = AllServices.Container.Get<ILoadingScreenProvider>();
        loadingScreenProvider.LoadAndDestroy(operationQueue);
    }

    public void RegisterOperation()
    {
        AllServices containerServices = AllServices.Container;
        containerServices.Register<EventBus>(new EventBus());
        containerServices.Register<ILoadingScreenProvider>(new LoadingScreenProvider());
        containerServices.Register<IAssetProvider>(new AssetProvider());
        containerServices.Register<IGameFactory>(new GameFactory());
        containerServices.Register<ISaveLoadService>(new SaveLoadService());
    }
}