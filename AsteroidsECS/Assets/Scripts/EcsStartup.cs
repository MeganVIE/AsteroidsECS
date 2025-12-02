using Aspects;
using Inputs;
using Inputs.Systems;
using Leopotam.EcsProto;
using Moving;
using Systems;
using UI;
using UnityEngine;
using Utils;

class EcsStartup : MonoBehaviour
{
    [SerializeField] private UnityInputService unityInputService;
    
    ProtoWorld _world;
    IProtoSystems _systems;

    void Start ()
    {
        _world = new ProtoWorld(new GameAspect());

        _systems = new ProtoSystems (_world);
        _systems
            // Модули должны быть зарегистрированы здесь.
            .AddModule(new MovingSystemsModule())

            // Системы вне модулей могут
            // быть зарегистрированы здесь.
            .AddSystem(new CameraDataInitSystem())
            .AddSystem(new MoveInputSystem())

            .AddSystem(new ShipCreateSystem())
            .AddSystem(new ShipViewSystem())

            // Сервисы могут быть добавлены в любом месте.
            .AddService(unityInputService, typeof(IInputService))
            .AddService(new DeltaTimeService(), typeof(IDeltaTimeService))
            .AddService(new ShipDataViewService(), typeof(IShipDataViewService))
            .AddService(new CameraDataService(), typeof(ICameraDataService));

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