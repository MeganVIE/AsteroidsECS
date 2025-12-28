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
    public class UFOSpawnSystem : IProtoInitSystem, IProtoRunSystem
    {
        private ProtoIt _cameraDataIt;
        
        private UFOAspect _ufoAspect;
        private ObjectIdAspect _objectIdAspect;
        
        private MovableAspect _movableAspect;
        private RotationAspect _rotationAspect;
        private MoveSpeedAspect _moveSpeedAspect;
        private CollisionRadiusAspect _collisionRadiusAspect;
        private CollisionTargetAspect _collisionTargetAspect;
        private ObjectTypeAspect _objectTypeAspect;
        private HealthAspect _healthAspect;
        private ShipFollowAspect _shipFollowAspect;
        private TeleportOutsideScreenAspect _teleportOutsideScreenAspect;
        
        private CameraDataComponent _cameraDataComponent;
        
        private IUFODataViewService _ufoDataViewService;
        private IDeltaTimeService _deltaTimeService;
        private IRandomService _randomService;
        private UFOConfig _ufoConfig;

        private float _respawnTime;
        private float _time;

        private int _lastId;

        public void Init(IProtoSystems systems)
        {
            _deltaTimeService = systems.GetService<IDeltaTimeService>();
            _randomService = systems.GetService<IRandomService>();
            _ufoDataViewService = systems.GetService<IUFODataViewService>();
            
            _ufoConfig = UFOConfig.LoadFromAssets();
            _respawnTime = _ufoConfig.DelaySpawnTime;
            
            var world = systems.World();
            _ufoAspect = world.GetAspect<UFOAspect>();
            _objectIdAspect = world.GetAspect<ObjectIdAspect>();
            _movableAspect = world.GetAspect<MovableAspect>();
            _rotationAspect = world.GetAspect<RotationAspect>();
            _moveSpeedAspect = world.GetAspect<MoveSpeedAspect>();
            _collisionRadiusAspect = world.GetAspect<CollisionRadiusAspect>();
            _collisionTargetAspect = world.GetAspect<CollisionTargetAspect>();
            _objectTypeAspect = world.GetAspect<ObjectTypeAspect>();
            _healthAspect = world.GetAspect<HealthAspect>();
            _shipFollowAspect = world.GetAspect<ShipFollowAspect>();
            _teleportOutsideScreenAspect = world.GetAspect<TeleportOutsideScreenAspect>();

            var cameraDataAspect = world.GetAspect<CameraDataAspect>();
            _cameraDataIt = new(new[] { typeof(CameraDataComponent) });
            _cameraDataIt.Init(world);
            
            foreach (ProtoEntity cameraEntity in _cameraDataIt)
            {
                _cameraDataComponent = cameraDataAspect.Pool.Get(cameraEntity);
            }
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
            _ufoAspect.Pool.NewEntity(out ProtoEntity entity);
            _teleportOutsideScreenAspect.Pool.Add(entity);
            _shipFollowAspect.Pool.Add(entity);
            _rotationAspect.Pool.Add(entity);

            ref ObjectIDComponent objectIDComponent = ref _objectIdAspect.Pool.Add(entity);
            
            ref MoveSpeedComponent moveSpeedComponent = ref _moveSpeedAspect.Pool.Add(entity);
            ref MovableComponent movableComponent = ref _movableAspect.Pool.Add(entity);

            ref CollisionRadiusComponent collisionRadiusComponent = ref _collisionRadiusAspect.Pool.Add(entity);
            ref CollisionTargetComponent collisionTargetComponent = ref _collisionTargetAspect.Pool.Add(entity);
            ref ObjectTypeComponent objectTypeComponent = ref _objectTypeAspect.Pool.Add(entity);
            ref HealthComponent healthComponent = ref _healthAspect.Pool.Add(entity);

            int id = ++_lastId;
            objectIDComponent.Id = id;
            
            var position = GetRandomPositionOnBound();
            movableComponent.Position = position;
            moveSpeedComponent.Value = _ufoConfig.StartMoveSpeed;

            collisionRadiusComponent.Value = _ufoConfig.CollisionRadius;
            collisionTargetComponent.Target = ObjectType.Ship;
            objectTypeComponent.ObjectType = ObjectType.Enemy;
            healthComponent.Value = 1;
            
            _ufoDataViewService!.CreateView(id, _ufoConfig);
            _ufoDataViewService.SetPosition(id, position);
        }

        private Point GetRandomPositionOnBound()
        {
            Point position = new Point();
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
            componentA = GetRandomFiftyFifty() ? limitA-.02f : -limitA+.02f;
            componentB = _randomService.GetRandom(-limitB+.02f, limitB-.02f);
        }
    }
}