using Aspects;
using Inputs;
using Leopotam.EcsProto;
using Systems;
using UnityEngine;

class EcsStartup : MonoBehaviour
{

    [SerializeField] private InputSystem _inputSystem;
    private IDeltaTimeSource _deltaTimeSource;
    private IShipDataViewProvider _shipDataViewProvider;
    
    ProtoWorld _world;
    IProtoSystems _systems;

    void Start ()
    {
        _deltaTimeSource = new DeltaTimeSource();
        _shipDataViewProvider = new ShipDataViewProvider();
        
        _world = new ProtoWorld(new GameAspect());

        _systems = new ProtoSystems (_world);
        _systems
            // Модули должны быть зарегистрированы здесь.
            // .AddModule (new TestModule1 ())
            // .AddModule (new TestModule2 ())

            // Системы вне модулей могут
            // быть зарегистрированы здесь.
            .AddSystem(new GameInitSystem(_shipDataViewProvider))
            .AddSystem(new ShipInputSystem(_inputSystem))
            .AddSystem(new AccelerationSystem(_deltaTimeSource))
            .AddSystem(new AccelerationMoveSystem())
            .AddSystem(new ShipViewSystem(_shipDataViewProvider))

            // Сервисы могут быть добавлены в любом месте.
            // .AddService (new TestService1 ())

            .Init();
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