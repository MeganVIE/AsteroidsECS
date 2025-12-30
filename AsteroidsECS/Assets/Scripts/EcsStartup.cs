using CameraData.Services;
using CameraData.Systems;
using Collisions.Systems;
using Destroy.Systems;
using Health.Systems;
using Inputs;
using Inputs.Services;
using Laser;
using Leopotam.EcsProto;
using Moving;
using Ship;
using Spawn.Systems;
using UFO;
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
            
            .AddModule(new ShipSystemsModule())
            .AddModule(new LaserSystemsModule())
            
            .AddModule(new UFOSystemsModule())

            // Системы вне модулей могут
            // быть зарегистрированы здесь.
            .AddSystem(new CameraDataInitSystem(), -100)
            .AddSystem(new CircleCollisionSystem(), -1)

            .AddSystem(new BulletSpawnSystem())
            .AddSystem(new AsteroidSpawnSystem())
            .AddSystem(new AsteroidPartSpawnSystem())

            .AddSystem(new BulletViewPositionSystem())
            .AddSystem(new AsteroidViewPositionSystem())
            .AddSystem(new AsteroidPartViewPositionSystem())
            
            .AddSystem(new DamageHandleSystem(), 100)
            
            .AddSystem(new DestroyOutsideScreenSystem())
            .AddSystem(new DestroyByTimerSystem())
            
            .AddSystem(new BulletDestroySystem(), 200)
            .AddSystem(new AsteroidsDestroySystem(), 205)
            .AddSystem(new AsteroidPartsDestroySystem(), 210)

            // Сервисы могут быть добавлены в любом месте.
            .AddService(new DeltaTimeService(), typeof(IDeltaTimeService))
            .AddService(new CameraDataService(), typeof(ICameraDataService))
            .AddService(new RandomService(), typeof(IRandomService))
            
            .AddService(new BulletDataViewService(), typeof(IBulletDataViewService))
            .AddService(new AsteroidDataViewService(), typeof(IAsteroidDataViewService))
            .AddService(new AsteroidPartDataViewService(), typeof(IAsteroidPartDataViewService))

            .AddService(_gameOverService, typeof(IGameOverService));

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