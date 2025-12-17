using Collisions.Aspects;
using Collisions.Components;
using Components;
using Configs;
using Data;
using EntityTags.Aspects;
using EntityTags.Components;
using Inputs.Aspects;
using Inputs.Components;
using Leopotam.EcsProto;
using Moving.Aspects;
using Moving.Components;
using UI.Services;
using Utils;

namespace Spawn.Systems
{
    public class BulletSpawnSystem : IProtoInitSystem, IProtoRunSystem
    {
        private BulletInputEventAspect _bulletInputEventAspect;
        private BulletAspect _bulletAspect;
        private MovableAspect _movableAspect;
        private RotationAspect _rotationAspect;
        private MoveSpeedAspect _moveSpeedAspect;
        private CollisionRadiusAspect _collisionRadiusAspect;
        private CollisionTargetAspect _collisionTargetAspect;
        private ObjectTypeAspect _objectTypeAspect;
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
            _movableAspect = world.GetAspect<MovableAspect>();
            _rotationAspect = world.GetAspect<RotationAspect>();
            _moveSpeedAspect = world.GetAspect<MoveSpeedAspect>();
            _collisionRadiusAspect = world.GetAspect<CollisionRadiusAspect>();
            _collisionTargetAspect = world.GetAspect<CollisionTargetAspect>();
            _objectTypeAspect = world.GetAspect<ObjectTypeAspect>();
            _bulletInputEventAspect = world.GetAspect<BulletInputEventAspect>();
            _destroyOutsideScreenAspect = world.GetAspect<DestroyOutsideScreenAspect>();
            
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
            ref BulletComponent bulletComponent = ref _bulletAspect.Pool.NewEntity(out ProtoEntity entity);
            _destroyOutsideScreenAspect.Pool.Add(entity);
            
            ref MoveSpeedComponent moveSpeedComponent = ref _moveSpeedAspect.Pool.Add(entity);
            ref MovableComponent movableComponent = ref _movableAspect.Pool.Add(entity);
            ref RotationComponent rotationComponent = ref _rotationAspect.Pool.Add(entity);

            ref CollisionRadiusComponent collisionRadiusComponent = ref _collisionRadiusAspect.Pool.Add(entity);
            ref CollisionTargetComponent collisionTargetComponent = ref _collisionTargetAspect.Pool.Add(entity);
            ref ObjectTypeComponent objectTypeComponent = ref _objectTypeAspect.Pool.Add(entity);

            int id = ++_lastId;
            bulletComponent.Id = id;
            
            movableComponent.Position = position;
            rotationComponent.Angle = rotation;
            moveSpeedComponent.Value = _bulletConfig.StartMoveSpeed;

            collisionRadiusComponent.CollisionRadius = _bulletConfig.CollisionRadius;
            collisionTargetComponent.Target = ObjectType.Asteroid;
            objectTypeComponent.ObjectType = ObjectType.Bullet;
            
            _bulletDataViewService.CreateView(id, _bulletConfig);
            _bulletDataViewService.SetPosition(id, position);
        }
    }
}