using CameraData.Aspects;
using CameraData.Components;
using Collisions.Aspects;
using Collisions.Components;
using Components;
using Configs;
using Data;
using EntityTags.Aspects;
using EntityTags.Components;
using Health.Aspects;
using Health.Components;
using Leopotam.EcsProto;
using Moving.Aspects;
using Moving.Components;
using UI.Services;
using Utils;

namespace Spawn.Systems
{
    public class AsteroidSpawnSystem : IProtoInitSystem, IProtoRunSystem
    {
        ProtoIt _cameraDataIt;
        
        AsteroidAspect _asteroidAspect;
        MovableAspect _movableAspect;
        RotationAspect _rotationAspect;
        MoveSpeedAspect _moveSpeedAspect;
        CollisionRadiusAspect _collisionRadiusAspect;
        CollisionTargetAspect _collisionTargetAspect;
        ObjectTypeAspect _objectTypeAspect;
        HealthAspect _healthAspect;
        private TeleportOutsideScreenAspect _teleportOutsideScreenAspect;
        
        CameraDataComponent _cameraDataComponent;
        
        private IAsteroidDataViewService _asteroidDataViewService;
        private IDeltaTimeService _deltaTimeService;
        private IRandomService _randomService;
        private AsteroidConfig _asteroidConfig;

        private float _respawnTime;
        private float _time;

        private int _lastId;

        public void Init(IProtoSystems systems)
        {
            _deltaTimeService = systems.GetService<IDeltaTimeService>();
            _randomService = systems.GetService<IRandomService>();
            _asteroidDataViewService = systems.GetService<IAsteroidDataViewService>();
            
            _asteroidConfig = AsteroidConfig.LoadFromAssets();
            _respawnTime = _asteroidConfig.DelaySpawnTime;
            
            var world = systems.World();
            _asteroidAspect = world.GetAspect<AsteroidAspect>();
            _movableAspect = world.GetAspect<MovableAspect>();
            _rotationAspect = world.GetAspect<RotationAspect>();
            _moveSpeedAspect = world.GetAspect<MoveSpeedAspect>();
            _collisionRadiusAspect = world.GetAspect<CollisionRadiusAspect>();
            _collisionTargetAspect = world.GetAspect<CollisionTargetAspect>();
            _objectTypeAspect = world.GetAspect<ObjectTypeAspect>();
            _healthAspect = world.GetAspect<HealthAspect>();
            _teleportOutsideScreenAspect = world.GetAspect<TeleportOutsideScreenAspect>();

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
            ref AsteroidComponent asteroidComponent = ref _asteroidAspect.Pool.NewEntity(out ProtoEntity entity);
            _teleportOutsideScreenAspect.Pool.Add(entity);
            
            ref MoveSpeedComponent moveSpeedComponent = ref _moveSpeedAspect.Pool.Add(entity);
            ref MovableComponent movableComponent = ref _movableAspect.Pool.Add(entity);
            ref RotationComponent rotationComponent = ref _rotationAspect.Pool.Add(entity);

            ref CollisionRadiusComponent collisionRadiusComponent = ref _collisionRadiusAspect.Pool.Add(entity);
            ref CollisionTargetComponent collisionTargetComponent = ref _collisionTargetAspect.Pool.Add(entity);
            ref ObjectTypeComponent objectTypeComponent = ref _objectTypeAspect.Pool.Add(entity);
            ref HealthComponent healthComponent = ref _healthAspect.Pool.Add(entity);

            int id = ++_lastId;
            asteroidComponent.Id = id;
            
            var position = GetRandomPositionOnBound();
            movableComponent.Position = position;
            rotationComponent.Angle = _randomService.GetRandom(0, 360);
            moveSpeedComponent.Value = _asteroidConfig.StartMoveSpeed;

            collisionRadiusComponent.CollisionRadius = _asteroidConfig.CollisionRadius;
            collisionTargetComponent.Target = ObjectType.Ship;
            objectTypeComponent.ObjectType = ObjectType.Asteroid;
            healthComponent.Value = 1;
            
            _asteroidDataViewService!.CreateView(id, _asteroidConfig);
            _asteroidDataViewService.SetPosition(id, position);
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