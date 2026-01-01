using Asteroids;
using Bullet;
using CameraData.Services;
using CameraData.Systems;
using Collisions.Systems;
using Destroy;
using Destroy.Systems;
using Health.Systems;
using Inputs;
using Inputs.Services;
using Laser;
using Leopotam.EcsProto;
using Moving;
using Score;
using Score.Systems;
using Ship;
using UFO;
using UI.Services;
using UI.UnityUI;
using UnityEngine;
using Utils;

class EcsStartup : MonoBehaviour
{
    [SerializeField] private UnityInputService unityInputService;
    [SerializeField] private GameOverPanel gameOverPanel;
    
    ProtoWorld _world;
    IProtoSystems _systems;

    IGameOverService _gameOverService;

    void Start ()
    {
        InitWorld();
    }

    private void InitWorld()
    {
        _world = new ProtoWorld(new GameAspectsModule());

        _gameOverService ??= new GameOverService();
        _gameOverService.SetPanel(gameOverPanel);

        _systems = new ProtoSystems (_world);
        _systems
            // Модули должны быть зарегистрированы здесь.
            .AddModule(new MovingSystemsModule())
            .AddModule(new InputsSystemsModule(unityInputService))
            
            .AddModule(new ShipSystemsModule())
            .AddModule(new LaserSystemsModule())
            .AddModule(new BulletSystemsModule())
            
            .AddModule(new AsteroidsSystemsModule())
            .AddModule(new UFOSystemsModule())
            
            .AddModule(new DestroySystemsModule())
            .AddModule(new ScoreSystemsModule())

            // Системы вне модулей могут быть зарегистрированы здесь.
            .AddSystem(new CameraDataInitSystem(), -100)
            .AddSystem(new CircleCollisionSystem(), -1)
            
            .AddSystem(new DamageHandleSystem(), 10)

            // Сервисы могут быть добавлены в любом месте.
            .AddService(new DeltaTimeService(), typeof(IDeltaTimeService))
            .AddService(new CameraDataService(), typeof(ICameraDataService))
            .AddService(new RandomService(), typeof(IRandomService))

            .AddService(_gameOverService, typeof(IGameOverService));

        _systems.Init();
    }

    void Update ()
    {
        if (_gameOverService.IsGameOver)
        {
            if (_gameOverService.IsRestartGame)
            {
                OnDestroy();
                InitWorld();
            }
            
            return;
        }
        
        _systems.Run();
    }

    void OnDestroy ()
    {
        _systems?.Destroy();
        _systems = null;
        _world?.Destroy();
        _world = null;
    }
}