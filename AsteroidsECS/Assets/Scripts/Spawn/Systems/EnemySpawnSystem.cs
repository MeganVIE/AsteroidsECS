using Collisions.Aspects;
using Collisions.Components;
using Common;
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
using Score.Aspects;
using Score.Components;
using UFO.Services;
using UIView.Services;
using Utils;

namespace Spawn.Systems
{
    public abstract class EnemySpawnSystem<TComponent, TEntityAspect, TViewService, TConfig> : IProtoInitSystem, IProtoRunSystem
        where TComponent: struct
        where TEntityAspect: AbstractAspect<TComponent>
        where TViewService: class, IDataViewService
        where TConfig: EnemyConfig
    {
        private TEntityAspect _entityAspect;
        private ObjectIdAspect _objectIdAspect;
        
        private MovableAspect _movableAspect;
        private RotationAspect _rotationAspect;
        private MoveSpeedAspect _moveSpeedAspect;
        private CollisionRadiusAspect _collisionRadiusAspect;
        private CollisionTargetAspect _collisionTargetAspect;
        private CollisionObjectTypeAspect _collisionObjectTypeAspect;
        private HealthAspect _healthAspect;
        private ScoreChangeByDestroyAspect _scoreChangeByDestroyAspect;
        private TeleportOutsideScreenAspect _teleportOutsideScreenAspect;
        
        private TViewService _viewService;
        
        protected IRandomService RandomService;
        protected TConfig Config;

        private int _lastId;

        public void Init(IProtoSystems systems)
        {
            _viewService = systems.GetService<TViewService>();
            RandomService = systems.GetService<IRandomService>();
            
            var world = systems.World();
            _entityAspect = world.GetAspect<TEntityAspect>();
            _objectIdAspect = world.GetAspect<ObjectIdAspect>();
            _movableAspect = world.GetAspect<MovableAspect>();
            _rotationAspect = world.GetAspect<RotationAspect>();
            _moveSpeedAspect = world.GetAspect<MoveSpeedAspect>();
            _collisionRadiusAspect = world.GetAspect<CollisionRadiusAspect>();
            _collisionTargetAspect = world.GetAspect<CollisionTargetAspect>();
            _collisionObjectTypeAspect = world.GetAspect<CollisionObjectTypeAspect>();
            _healthAspect = world.GetAspect<HealthAspect>();
            _scoreChangeByDestroyAspect = world.GetAspect<ScoreChangeByDestroyAspect>();
            _teleportOutsideScreenAspect = world.GetAspect<TeleportOutsideScreenAspect>();

            PostInit(systems);
        }

        public abstract void Run();

        protected abstract void PostInit(IProtoSystems systems);

        protected void Spawn(Point position)
        {
            _entityAspect.Pool.NewEntity(out ProtoEntity entity);
            _teleportOutsideScreenAspect.Pool.Add(entity);

            int id = ++_lastId;
            
            ref ObjectIDComponent objectIDComponent = ref _objectIdAspect.Pool.Add(entity);
            objectIDComponent.Id = id;

            SetCollisionData(entity);
            SetMoveSpeedData(entity);
            SetHealthData(entity);
            SetScoreData(entity);
            SetTransformData(entity, position);
            SetViewData(id, position);

            OnSpawn(entity);
        }
        
        protected virtual void OnSpawn(ProtoEntity entity) { }

        private void SetScoreData(ProtoEntity entity)
        {
            ref ScoreChangeByDestroyComponent component = ref _scoreChangeByDestroyAspect.Pool.Add(entity);
            component.Amount = Config.ScorePoints;
        }

        private void SetHealthData(ProtoEntity entity)
        {
            ref HealthComponent healthComponent = ref _healthAspect.Pool.Add(entity);
            healthComponent.Value = 1;
        }

        private void SetTransformData(ProtoEntity entity, Point position)
        {
            ref RotationComponent rotationComponent = ref _rotationAspect.Pool.Add(entity);
            rotationComponent.Angle = RandomService.GetRandom(0, 360);
            ref MovableComponent movableComponent = ref _movableAspect.Pool.Add(entity);
            movableComponent.Position = position;
        }

        private void SetMoveSpeedData(ProtoEntity entity)
        {
            ref MoveSpeedComponent moveSpeedComponent = ref _moveSpeedAspect.Pool.Add(entity);
            moveSpeedComponent.Value = Config.StartMoveSpeed;
        }

        private void SetCollisionData(ProtoEntity entity)
        {
            ref CollisionRadiusComponent collisionRadiusComponent = ref _collisionRadiusAspect.Pool.Add(entity);
            ref CollisionObjectTypeComponent collisionObjectTypeComponent = ref _collisionObjectTypeAspect.Pool.Add(entity);
            ref CollisionTargetComponent collisionTargetComponent = ref _collisionTargetAspect.Pool.Add(entity);
            
            collisionRadiusComponent.Value = Config.CollisionRadius;
            collisionTargetComponent.Target = ObjectType.Ship;
            collisionObjectTypeComponent.ObjectType = ObjectType.Enemy;
        }

        private void SetViewData(int id, Point position)
        {
            _viewService.CreateView(id, Config);
            _viewService.SetPosition(id, position);
        }
    }
}