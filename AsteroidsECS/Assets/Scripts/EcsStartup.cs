using CameraData.Services;
using CameraData.Systems;
using Collisions.Systems;
using Health.Systems;
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
    [SerializeField] private GameObject gameOverPanel;
    
    ProtoWorld _world;
    IProtoSystems _systems;

    IGameOverService _gameOverService;

    void Start ()
    {
        _world = new ProtoWorld(new GameAspectsModule());

        _gameOverService = new GameOverService();
        _gameOverService.SetPanel(gameOverPanel);

        _systems = new ProtoSystems (_world);
        _systems
            // Модули должны быть зарегистрированы здесь.
            .AddModule(new MovingSystemsModule())
            .AddModule(new InputsSystemsModule(unityInputService))

            // Системы вне модулей могут
            // быть зарегистрированы здесь.
            .AddSystem(new CameraDataInitSystem(), -100)

            .AddSystem(new MovableCollisionSystem(), 1)

            .AddSystem(new AsteroidSpawnSystem())
            .AddSystem(new ShipSpawnSystem())

            .AddSystem(new ShipViewSystem())
            .AddSystem(new AsteroidViewSystem())
            
            .AddSystem(new ShipDamageHandleSystem(), 100)

            // Сервисы могут быть добавлены в любом месте.
            .AddService(new DeltaTimeService(), typeof(IDeltaTimeService))
            .AddService(new CameraDataService(), typeof(ICameraDataService))
            .AddService(new RandomService(), typeof(IRandomService))

            .AddService(_gameOverService, typeof(IGameOverService))
            
            .AddService(new ShipDataViewService(), typeof(IShipDataViewService))
            .AddService(new AsteroidDataViewService(), typeof(IAsteroidDataViewService));

        _systems.Init();
    }

    void Update ()
    {
        if (!_gameOverService.IsGameOver)
            _systems.Run();
    }

    void OnDestroy () 
    {
        _systems?.Destroy ();
        _systems = null;
        _world?.Destroy ();
        _world = null;
    }
}