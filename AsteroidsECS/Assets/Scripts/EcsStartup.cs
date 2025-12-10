using CameraData.Services;
using CameraData.Systems;
using Inputs;
using Inputs.Services;
using Leopotam.EcsProto;
using Moving;
using Spawn.Systems;
using UI.Services;
using UI.Systems;
using UnityEngine;
using Utils;

class EcsStartup : MonoBehaviour
{
    [SerializeField] private UnityInputService unityInputService;
    
    ProtoWorld _world;
    IProtoSystems _systems;

    void Start ()
    {
        _world = new ProtoWorld(new GameAspectsModule());

        _systems = new ProtoSystems (_world);
        _systems
            // Модули должны быть зарегистрированы здесь.
            .AddModule(new MovingSystemsModule())
            .AddModule(new InputsSystemsModule(unityInputService))

            // Системы вне модулей могут
            // быть зарегистрированы здесь.
            .AddSystem(new CameraDataInitSystem(), -100)

            .AddSystem(new AsteroidSpawnSystem())
            .AddSystem(new ShipSpawnSystem())
            
            .AddSystem(new ShipViewSystem())
            .AddSystem(new AsteroidViewSystem())

            // Сервисы могут быть добавлены в любом месте.
            .AddService(new DeltaTimeService(), typeof(IDeltaTimeService))
            .AddService(new CameraDataService(), typeof(ICameraDataService))
            .AddService(new RandomService(), typeof(IRandomService))

            .AddService(new ShipDataViewService(), typeof(IShipDataViewService))
            .AddService(new AsteroidDataViewService(), typeof(IAsteroidDataViewService));

        _systems.Init();
    }

    void Update () {
        _systems.Run ();
    }

    void OnDestroy () {
        _systems?.Destroy ();
        _systems = null;
        _world?.Destroy ();
        _world = null;
    }
}