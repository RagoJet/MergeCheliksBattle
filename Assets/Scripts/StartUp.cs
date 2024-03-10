using System.Collections.Generic;
using Configs;
using Operations;
using Operations.SceneLoadingOperations;
using Services;
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
        RegisterOperation();
        Queue<ILoadingOperation> operationQueue = new Queue<ILoadingOperation>();
        operationQueue.Enqueue(new LoadingDataProgressOperation());
        operationQueue.Enqueue(new GameSceneLoadingOperation());

        ILoadingScreenProvider loadingScreenProvider = AllServices.Container.Get<ILoadingScreenProvider>();
        loadingScreenProvider.LoadAndDestroy(operationQueue);
    }

    public void RegisterOperation()
    {
        AllServices containerServices = AllServices.Container;

        containerServices.Register<ISaveLoadService>(new SaveLoadService());
        containerServices.Register<EventBus>(new EventBus());
        containerServices.Register<ILoadingScreenProvider>(new LoadingScreenProvider());
        containerServices.Register<IAssetProvider>(new AssetProvider());
        containerServices.Register<IStaticDataFactory>(new StaticDataFactory());
        containerServices.Register<IGameFactory>(new GameFactory());
        containerServices.Register<IAudioService>(new AudioService());

        MyJoyStick myJoyStick = containerServices.Get<IGameFactory>().CreateMyJoystick();
        myJoyStick.SwitchOff();
        AllServices.Container.Register<IJoyStick>(myJoyStick);
    }
}