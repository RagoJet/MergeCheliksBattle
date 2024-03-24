using System.Collections.Generic;
using Operations;
using Operations.SceneLoadingOperations;
using Services;
using Services.Ads;
using Services.AssetManagement;
using Services.Audio;
using Services.Factories;
using Services.JoySticks;
using Services.LoadingScreenNS;
using Services.SaveLoad;
using UnityEngine;

public class StartUp : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
        RegisterServices();
        Queue<ILoadingOperation> operationQueue = new Queue<ILoadingOperation>();
        operationQueue.Enqueue(new LoadingDataProgressOperation());
        operationQueue.Enqueue(new GameSceneLoadingOperation());

        ILoadingScreenProvider loadingScreenProvider = AllServices.Container.Get<ILoadingScreenProvider>();
        loadingScreenProvider.LoadAndDestroy(operationQueue);
    }

    private void RegisterServices()
    {
        AllServices containerServices = AllServices.Container;

        containerServices.Register<ISaveLoadService>(new SaveLoadService());
        containerServices.Register<IAdsService>(new AdsService());
        containerServices.Register<EventBus>(new EventBus());
        containerServices.Register<ILoadingScreenProvider>(new LoadingScreenProvider());
        containerServices.Register<IAssetProvider>(new AssetProvider());
        containerServices.Register<IStaticDataFactory>(new StaticDataFactory());
        containerServices.Register<IGameFactory>(new GameFactory());
        containerServices.Register<IAudioService>(new AudioService());

        AllServices.Container.Register<IJoyStick>(containerServices.Get<IGameFactory>().CreateMyJoystick());
    }
}