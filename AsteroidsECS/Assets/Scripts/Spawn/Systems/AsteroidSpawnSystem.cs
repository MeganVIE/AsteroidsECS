using CameraData.Aspects;
using CameraData.Components;
using Components;
using Configs;
using EntityTags.Aspects;
using EntityTags.Components;
using Leopotam.EcsProto;
using Moving.Aspects;
using Moving.Components;
using UI.Services;
using Utils;

namespace Spawn.Systems
{
    public class AsteroidSpawnSystem : IProtoInitSystem, IProtoRunSystem
    {
        IProtoSystems _systems;
        ProtoIt _cameraDataIt;
        
        ProtoPool<AsteroidComponent> _asteroidComponentPool;
        ProtoPool<MovableComponent> _movableComponentPool;
        ProtoPool<RotationComponent> _rotationComponentPool;
        ProtoPool<MoveSpeedComponent> _moveSpeedComponentPool;
        
        CameraDataComponent _cameraDataComponent;
        
        private IDeltaTimeService _deltaTimeService;
        private IRandomService _randomService;
        private AsteroidConfig _asteroidConfig;

        private float _respawnTime;
        private float _time;

        private int _lastId;

        public void Init(IProtoSystems systems)
        {
            _systems = systems;
            _deltaTimeService = _systems.GetService<IDeltaTimeService>();
            _randomService = _systems.GetService<IRandomService>();
            
            _asteroidConfig = AsteroidConfig.LoadFromAssets();
            _respawnTime = _asteroidConfig.DelaySpawnTime;
            
            var world = _systems.World();
            AsteroidAspect asteroidAspect = world.GetAspect<AsteroidAspect>();
            _asteroidComponentPool = asteroidAspect.Pool;
            MovableAspect movableAspect = world.GetAspect<MovableAspect>();
            _movableComponentPool = movableAspect.Pool;
            RotationAspect rotationAspect = world.GetAspect<RotationAspect>();
            _rotationComponentPool = rotationAspect.Pool;
            MoveSpeedAspect moveSpeedAspect = world.GetAspect<MoveSpeedAspect>();
            _moveSpeedComponentPool = moveSpeedAspect.Pool;

            var cameraDataAspect = world.GetAspect<CameraDataAspect>();
            _cameraDataIt = new(new[] { typeof(CameraDataComponent) });
            _cameraDataIt.Init(world);
            
            foreach (ProtoEntity cameraEntity in _cameraDataIt)
            {
                _cameraDataComponent = cameraDataAspect.Pool.Get(cameraEntity);
            }

            Spawn();
        }
        
        public void Run()
        {
            _time += _deltaTimeService.DeltaTime;

            if (_respawnTime > _time) 
                return;
            
            Spawn();
            _time = 0;
        }

        private void Spawn()
        {
            ref AsteroidComponent asteroidComponent = ref _asteroidComponentPool.NewEntity(out ProtoEntity entity);
            
            ref MoveSpeedComponent moveSpeedComponent = ref _moveSpeedComponentPool.Add(entity);
            ref MovableComponent movableComponent = ref _movableComponentPool.Add(entity);
            ref RotationComponent rotationComponent = ref _rotationComponentPool.Add(entity);

            int id = ++_lastId;
            asteroidComponent.Id = id;
            
            var position = GetRandomPositionOnBound();
            movableComponent.Position = position;
            rotationComponent.Angle = _randomService.GetRandom(0, 360);
            moveSpeedComponent.Value = _asteroidConfig.StartMoveSpeed;
            
            var asteroidDataViewService = _systems.GetService<IAsteroidDataViewService>();
            asteroidDataViewService!.CreateView(id, _asteroidConfig);
            asteroidDataViewService.SetPosition(id, position);
        }

        private Point GetRandomPositionOnBound()
        {
            Point position=new Point();
            if (GetRandomFiftyFifty())
                SetVectorComponentsValue(out position.X, out position.Y, 
                    _cameraDataComponent.HalfViewportWidth, _cameraDataComponent.HalfViewportHeight);
            else
                SetVectorComponentsValue(out position.Y, out position.X, 
                    _cameraDataComponent.HalfViewportHeight, _cameraDataComponent.HalfViewportWidth);
            return position;
        }

        private bool GetRandomFiftyFifty()
        {
            return _randomService.GetRandom(0, 2) == 0;
        }
    
        private void SetVectorComponentsValue(out float componentA, out float componentB, float limitA, float limitB)
        {
            componentA = GetRandomFiftyFifty() ? limitA : -limitA;
            componentB = _randomService.GetRandom(-limitB, limitB);
        }
    }
}