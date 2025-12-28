using Collisions.Aspects;
using Collisions.Components;
using Configs;
using Data;
using Destroy.Aspects;
using Destroy.Components;
using EntityTags.Aspects;
using EntityTags.Components;
using Inputs.Aspects;
using Inputs.Components;
using Laser.Aspects;
using Laser.Components;
using Laser.Services;
using Leopotam.EcsProto;
using Moving.Aspects;
using Moving.Components;
using Utils;

namespace Laser.Systems
{
    public class LaserSpawnSystem : IProtoInitSystem, IProtoRunSystem
    {
        private LaserInputEventAspect _laserInputEventAspect;
        private LaserAspect _laserAspect;
        private ObjectIdAspect _objectIdAspect;
        private MovableAspect _movableAspect;
        private RotationAspect _rotationAspect;
        private CollisionLengthAspect _collisionLengthAspect;
        private CollisionTargetAspect _collisionTargetAspect;
        private ObjectTypeAspect _objectTypeAspect;
        private DestroyByTimerAspect _destroyByTimerAspect;

        private LaserAmountAspect _laserAmountAspect;
        private LaserAmountLimitAspect _laserAmountLimitAspect;

        private ProtoIt _it;
        private ProtoIt _amountIt;

        private ILaserDataViewService _laserDataViewService;
        private LaserConfig _laserConfig;
        
        private float _time;
        private int _lastId;
        
        public void Init(IProtoSystems systems)
        {
            _laserConfig = LaserConfig.LoadFromAssets();
            
            var world = systems.World();
            _laserAspect = world.GetAspect<LaserAspect>();
            _objectIdAspect = world.GetAspect<ObjectIdAspect>();
            _movableAspect = world.GetAspect<MovableAspect>();
            _rotationAspect = world.GetAspect<RotationAspect>();
            _collisionLengthAspect = world.GetAspect<CollisionLengthAspect>();
            _collisionTargetAspect = world.GetAspect<CollisionTargetAspect>();
            _objectTypeAspect = world.GetAspect<ObjectTypeAspect>();
            _destroyByTimerAspect = world.GetAspect<DestroyByTimerAspect>();
            _laserInputEventAspect = world.GetAspect<LaserInputEventAspect>();

            _laserAmountAspect = world.GetAspect<LaserAmountAspect>();
            _laserAmountLimitAspect = world.GetAspect<LaserAmountLimitAspect>();
            
            _laserDataViewService = systems.GetService<ILaserDataViewService>();

            _laserAmountAspect.Pool.NewEntity(out ProtoEntity entity);
            ref LaserAmountLimitComponent laserAmountLimitComponent = ref _laserAmountLimitAspect.Pool.Add(entity);
            laserAmountLimitComponent.Value = _laserConfig.MaxAmount;

            _it = new(new[] { typeof(LaserInputEventComponent), typeof(MovableComponent), typeof(RotationComponent) });
            _it.Init(world);

            _amountIt = new(new[] { typeof(LaserAmountComponent) });
            _amountIt.Init(world); 
        }

        public void Run()
        {
            foreach (var entity in _it)
            {
                LaserInputEventComponent laserInputEventComponent = _laserInputEventAspect.Pool.Get(entity);

                if (laserInputEventComponent.IsLaserPressing)
                {
                    foreach (var amountEntity in _amountIt)
                    {
                        ref LaserAmountComponent laserAmountComponent = ref _laserAmountAspect.Pool.Get(amountEntity);
                        LaserAmountLimitComponent laserAmountLimitComponent = _laserAmountLimitAspect.Pool.Get(amountEntity);

                        if (laserAmountComponent.Value < laserAmountLimitComponent.Value)
                        {
                            MovableComponent movableComponent = _movableAspect.Pool.Get(entity);
                            RotationComponent rotationComponent = _rotationAspect.Pool.Get(entity);
                    
                            Spawn(movableComponent.Position, rotationComponent.Angle);
                            laserAmountComponent.Value--;
                        }
                    }
                }
            }
        }

        private void Spawn(Point position, float rotation)
        {
            _laserAspect.Pool.NewEntity(out ProtoEntity entity);
            
            ref ObjectIDComponent objectIDComponent = ref _objectIdAspect.Pool.Add(entity);
            
            ref MovableComponent movableComponent = ref _movableAspect.Pool.Add(entity);
            ref RotationComponent rotationComponent = ref _rotationAspect.Pool.Add(entity);

            ref CollisionLengthComponent collisionLengthComponent = ref _collisionLengthAspect.Pool.Add(entity);
            ref CollisionTargetComponent collisionTargetComponent = ref _collisionTargetAspect.Pool.Add(entity);
            ref ObjectTypeComponent objectTypeComponent = ref _objectTypeAspect.Pool.Add(entity);

            ref DestroyByTimerComponent destroyByTimerComponent = ref _destroyByTimerAspect.Pool.Add(entity);

            int id = ++_lastId;
            objectIDComponent.Id = id;
            
            movableComponent.Position = position;
            rotationComponent.Angle = rotation;

            float collisionLength = _laserConfig.CollisionLength;
            collisionLengthComponent.Value = collisionLength;
            collisionTargetComponent.Target = ObjectType.Enemy;
            objectTypeComponent.ObjectType = ObjectType.Bullet;

            destroyByTimerComponent.LifeTime = _laserConfig.LifeTime;
            
            _laserDataViewService.CreateView(id, _laserConfig);
            _laserDataViewService.SetPosition(id, position);
            _laserDataViewService.SetRotation(id, rotation);
            _laserDataViewService.SetLength(id, collisionLength);
        }
    }
}