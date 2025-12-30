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
using UFO.Aspects;
using UFO.Services;
using Utils;

namespace UFO.Systems
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
        private CollisionObjectTypeAspect _collisionObjectTypeAspect;
        private HealthAspect _healthAspect;
        private ShipFollowAspect _shipFollowAspect;
        private TeleportOutsideScreenAspect _teleportOutsideScreenAspect;
        
        private CameraDataComponent _cameraDataComponent;
        
        private IUFODataViewService _ufoDataViewService;
        private IDeltaTimeService _deltaTimeService;
        private IRandomService _randomService;
        private UFOConfig _ufoConfig;

        private ProtoWorld _world;

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
            
            _world = systems.World();
            _ufoAspect = _world.GetAspect<UFOAspect>();
            _objectIdAspect = _world.GetAspect<ObjectIdAspect>();
            _movableAspect = _world.GetAspect<MovableAspect>();
            _rotationAspect = _world.GetAspect<RotationAspect>();
            _moveSpeedAspect = _world.GetAspect<MoveSpeedAspect>();
            _collisionRadiusAspect = _world.GetAspect<CollisionRadiusAspect>();
            _collisionTargetAspect = _world.GetAspect<CollisionTargetAspect>();
            _collisionObjectTypeAspect = _world.GetAspect<CollisionObjectTypeAspect>();
            _healthAspect = _world.GetAspect<HealthAspect>();
            _shipFollowAspect = _world.GetAspect<ShipFollowAspect>();
            _teleportOutsideScreenAspect = _world.GetAspect<TeleportOutsideScreenAspect>();

            var cameraDataAspect = _world.GetAspect<CameraDataAspect>();
            _cameraDataIt = new(new[] { typeof(CameraDataComponent) });
            _cameraDataIt.Init(_world);
            
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

            int id = ++_lastId;
            var position = GetRandomPositionOnBound();
            
            ref ObjectIDComponent objectIDComponent = ref _objectIdAspect.Pool.Add(entity);
            objectIDComponent.Id = id;

            SetCollisionData(entity);
            SetMoveSpeedData(entity);
            SetHealthData(entity);
            SetTransformData(entity, position);
            SetViewData(id, position);
        }

        private void SetHealthData(ProtoEntity entity)
        {
            ref HealthComponent healthComponent = ref _healthAspect.Pool.Add(entity);
            healthComponent.Value = 1;
        }

        private void SetTransformData(ProtoEntity entity, Point position)
        {
            _rotationAspect.Pool.Add(entity);
            ref MovableComponent movableComponent = ref _movableAspect.Pool.Add(entity);
            movableComponent.Position = position;
        }

        private void SetMoveSpeedData(ProtoEntity entity)
        {
            ref MoveSpeedComponent moveSpeedComponent = ref _moveSpeedAspect.Pool.Add(entity);
            moveSpeedComponent.Value = _ufoConfig.StartMoveSpeed;
        }

        private void SetCollisionData(ProtoEntity entity)
        {
            ref CollisionRadiusComponent collisionRadiusComponent = ref _collisionRadiusAspect.Pool.Add(entity);
            ref CollisionObjectTypeComponent collisionObjectTypeComponent = ref _collisionObjectTypeAspect.Pool.Add(entity);
            ref CollisionTargetComponent collisionTargetComponent = ref _collisionTargetAspect.Pool.Add(entity);
            
            collisionRadiusComponent.Value = _ufoConfig.CollisionRadius;
            collisionTargetComponent.Target = ObjectType.Ship;
            collisionObjectTypeComponent.ObjectType = ObjectType.Enemy;
        }

        private void SetViewData(int id, Point position)
        {
            _ufoDataViewService.CreateView(id, _ufoConfig);
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
            const float borderThreshold = .02f;
            componentA = GetRandomFiftyFifty() ? limitA - borderThreshold : -limitA + borderThreshold;
            componentB = _randomService.GetRandom(-limitB + borderThreshold, limitB - borderThreshold);
        }
    }
}