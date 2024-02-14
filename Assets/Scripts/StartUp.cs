using System.Collections.Generic;
using Factories;
using LoadingScreenNS;
using Operations;
using Operations.SceneLoadingOperations;
using Services;
using Services.SaveLoad;
using UnityEngine;

public class StartUp : MonoBehaviour
{
    private void Awake()
    {
        RegisterOperation();

        Queue<ILoadingOperation> operationQueue = new Queue<ILoadingOperation>();
        operationQueue.Enqueue(new MainMenuLoadingOperation());
        operationQueue.Enqueue(new LoadingDataProgressOperation());

        ILoadingScreenProvider loadingScreenProvider = AllServices.Container.Get<ILoadingScreenProvider>();
        loadingScreenProvider.LoadAndDestroy(operationQueue);
    }

    public void RegisterOperation()
    {
        AllServices containerServices = AllServices.Container;
        containerServices.Register<ILoadingScreenProvider>(new LoadingScreenProvider());
        containerServices.Register<IGameFactory>(new GameFactory());
        containerServices.Register<ISaveLoadService>(new SaveLoadService());
    }
}