using Bullet.Aspects;
using Bullet.Services;
using Collisions.Aspects;
using Collisions.Components;
using Components;
using Configs;
using Data;
using Destroy.Aspects;
using EntityTags.Aspects;
using EntityTags.Components;
using Health.Aspects;
using Health.Components;
using Inputs.Aspects;
using Inputs.Components;
using Leopotam.EcsProto;
using Moving.Aspects;
using Moving.Components;
using Utils;

namespace Bullet.Systems
{
    public class BulletSpawnSystem : IProtoInitSystem, IProtoRunSystem
    {
        private BulletInputEventAspect _bulletInputEventAspect;
        private BulletAspect _bulletAspect;
        private ObjectIdAspect _objectIdAspect;
        private MovableAspect _movableAspect;
        private RotationAspect _rotationAspect;
        private MoveSpeedAspect _moveSpeedAspect;
        private CollisionRadiusAspect _collisionRadiusAspect;
        private CollisionTargetAspect _collisionTargetAspect;
        private CollisionObjectTypeAspect _collisionObjectTypeAspect;
        private HealthAspect _healthAspect;
        private DestroyOutsideScreenAspect _destroyOutsideScreenAspect;

        private ProtoIt _it;

        private IBulletDataViewService _bulletDataViewService;
        private BulletConfig _bulletConfig;
        
        private float _time;
        private int _lastId;
        
        public void Init(IProtoSystems systems)
        {
            _bulletConfig = BulletConfig.LoadFromAssets();
            
            var world = systems.World();
            _bulletAspect = world.GetAspect<BulletAspect>();
            _objectIdAspect = world.GetAspect<ObjectIdAspect>();
            _movableAspect = world.GetAspect<MovableAspect>();
            _rotationAspect = world.GetAspect<RotationAspect>();
            _moveSpeedAspect = world.GetAspect<MoveSpeedAspect>();
            _collisionRadiusAspect = world.GetAspect<CollisionRadiusAspect>();
            _collisionTargetAspect = world.GetAspect<CollisionTargetAspect>();
            _collisionObjectTypeAspect = world.GetAspect<CollisionObjectTypeAspect>();
            _healthAspect = world.GetAspect<HealthAspect>();
            _destroyOutsideScreenAspect = world.GetAspect<DestroyOutsideScreenAspect>();
            _bulletInputEventAspect = world.GetAspect<BulletInputEventAspect>();
            
            _bulletDataViewService = systems.GetService<IBulletDataViewService>();

            _it = new(new[] { typeof(BulletInputEventComponent), typeof(MovableComponent), typeof(RotationComponent) });
            _it.Init(world);
        }

        public void Run()
        {
            foreach (var entity in _it)
            {
                BulletInputEventComponent bulletInputEventComponent = _bulletInputEventAspect.Pool.Get(entity);

                if (bulletInputEventComponent.IsGunPressing)
                {
            
                    MovableComponent movableComponent = _movableAspect.Pool.Get(entity);
                    RotationComponent rotationComponent = _rotationAspect.Pool.Get(entity);
                    
                    Spawn(movableComponent.Position, rotationComponent.Angle);
                }
            }
        }

        private void Spawn(Point position, float rotation)
        {
            _bulletAspect.Pool.NewEntity(out ProtoEntity entity);
            _destroyOutsideScreenAspect.Pool.Add(entity);
            
            ref ObjectIDComponent objectIDComponent = ref _objectIdAspect.Pool.Add(entity);
            
            ref MoveSpeedComponent moveSpeedComponent = ref _moveSpeedAspect.Pool.Add(entity);
            ref MovableComponent movableComponent = ref _movableAspect.Pool.Add(entity);
            ref RotationComponent rotationComponent = ref _rotationAspect.Pool.Add(entity);

            ref CollisionRadiusComponent collisionRadiusComponent = ref _collisionRadiusAspect.Pool.Add(entity);
            ref CollisionTargetComponent collisionTargetComponent = ref _collisionTargetAspect.Pool.Add(entity);
            ref CollisionObjectTypeComponent collisionObjectTypeComponent = ref _collisionObjectTypeAspect.Pool.Add(entity);
            ref HealthComponent healthComponent = ref _healthAspect.Pool.Add(entity);

            int id = ++_lastId;
            objectIDComponent.Id = id;
            
            movableComponent.Position = position;
            rotationComponent.Angle = rotation;
            moveSpeedComponent.Value = _bulletConfig.StartMoveSpeed;

            collisionRadiusComponent.Value = _bulletConfig.CollisionRadius;
            collisionTargetComponent.Target = ObjectType.Enemy;
            collisionObjectTypeComponent.ObjectType = ObjectType.Bullet;
            healthComponent.Value = 1;
            
            _bulletDataViewService.CreateView(id, _bulletConfig);
            _bulletDataViewService.SetPosition(id, position);
        }
    }
}