using Collisions.Aspects;
using Collisions.Components;
using Configs;
using Data;
using Destroy.Aspects;
using Destroy.Components;
using EntityTags.Aspects;
using EntityTags.Components;
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
        private LaserAspect _laserAspect;
        private ObjectIdAspect _objectIdAspect;
        private MovableAspect _movableAspect;
        private RotationAspect _rotationAspect;
        private CollisionLengthAspect _collisionLengthAspect;
        private CollisionTargetAspect _collisionTargetAspect;
        private CollisionObjectTypeAspect _collisionObjectTypeAspect;
        private DestroyByTimerAspect _destroyByTimerAspect;

        private SpawnLaserAspect _spawnLaserAspect;
        private LaserAmountAspect _laserAmountAspect;

        private ProtoIt _it;
        private ProtoIt _amountIt;

        private ILaserDataViewService _laserDataViewService;
        private LaserConfig _laserConfig;
        
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
            _collisionObjectTypeAspect = world.GetAspect<CollisionObjectTypeAspect>();
            _destroyByTimerAspect = world.GetAspect<DestroyByTimerAspect>();
            _laserAmountAspect = world.GetAspect<LaserAmountAspect>();
            _spawnLaserAspect = world.GetAspect<SpawnLaserAspect>();
            
            _laserDataViewService = systems.GetService<ILaserDataViewService>();

            _it = new(new[] { typeof(SpawnLaserComponent)});
            _it.Init(world);

            _amountIt = new(new[] { typeof(LaserAmountComponent) });
            _amountIt.Init(world); 
        }

        public void Run()
        {
            foreach (var entity in _it)
            {
                foreach (var amountEntity in _amountIt)
                {
                    ref LaserAmountComponent laserAmountComponent = ref _laserAmountAspect.Pool.Get(amountEntity);

                    if (laserAmountComponent.Value > 0)
                    {
                        SpawnLaserComponent spawnLaserComponent = _spawnLaserAspect.Pool.Get(entity);
                        Spawn(spawnLaserComponent.Position, spawnLaserComponent.Rotation);
                        laserAmountComponent.Value--;
                        _spawnLaserAspect.Pool.Del(entity);
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
            ref CollisionObjectTypeComponent collisionObjectTypeComponent = ref _collisionObjectTypeAspect.Pool.Add(entity);

            ref DestroyByTimerComponent destroyByTimerComponent = ref _destroyByTimerAspect.Pool.Add(entity);

            int id = ++_lastId;
            objectIDComponent.Id = id;
            
            movableComponent.Position = position;
            rotationComponent.Angle = rotation;

            float collisionLength = _laserConfig.CollisionLength;
            collisionLengthComponent.Value = collisionLength;
            collisionTargetComponent.Target = ObjectType.Enemy;
            collisionObjectTypeComponent.ObjectType = ObjectType.Bullet;

            destroyByTimerComponent.LifeTime = _laserConfig.LifeTime;
            
            _laserDataViewService.CreateView(id, _laserConfig);
            _laserDataViewService.SetPosition(id, position);
            _laserDataViewService.SetRotation(id, rotation);
            _laserDataViewService.SetLength(id, collisionLength);
        }
    }
}