using Collisions.Aspects;
using Collisions.Components;
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
    public class LaserSpawnSystem : IProtoInitSystem, IProtoRunSystem
    {
        private LaserInputEventAspect _laserInputEventAspect;
        private LaserAspect _laserAspect;
        private ObjectIdAspect _objectIdAspect;
        private MovableAspect _movableAspect;
        private RotationAspect _rotationAspect;
        private CollisionTargetAspect _collisionTargetAspect;
        private ObjectTypeAspect _objectTypeAspect;

        private ProtoIt _it;

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
            _collisionTargetAspect = world.GetAspect<CollisionTargetAspect>();
            _objectTypeAspect = world.GetAspect<ObjectTypeAspect>();
            _laserInputEventAspect = world.GetAspect<LaserInputEventAspect>();
            
            _laserDataViewService = systems.GetService<ILaserDataViewService>();

            _it = new(new[] { typeof(LaserInputEventComponent), typeof(MovableComponent), typeof(RotationComponent) });
            _it.Init(world);
        }

        public void Run()
        {
            foreach (var entity in _it)
            {
                LaserInputEventComponent laserInputEventComponent = _laserInputEventAspect.Pool.Get(entity);

                if (laserInputEventComponent.IsLaserPressing)
                {
            
                    MovableComponent movableComponent = _movableAspect.Pool.Get(entity);
                    RotationComponent rotationComponent = _rotationAspect.Pool.Get(entity);
                    
                    Spawn(movableComponent.Position, rotationComponent.Angle);
                }
            }
        }

        private void Spawn(Point position, float rotation)
        {
            _laserAspect.Pool.NewEntity(out ProtoEntity entity);
            
            ref ObjectIDComponent objectIDComponent = ref _objectIdAspect.Pool.Add(entity);
            
            ref MovableComponent movableComponent = ref _movableAspect.Pool.Add(entity);
            ref RotationComponent rotationComponent = ref _rotationAspect.Pool.Add(entity);

            ref CollisionTargetComponent collisionTargetComponent = ref _collisionTargetAspect.Pool.Add(entity);
            ref ObjectTypeComponent objectTypeComponent = ref _objectTypeAspect.Pool.Add(entity);

            int id = ++_lastId;
            objectIDComponent.Id = id;
            
            movableComponent.Position = position;
            rotationComponent.Angle = rotation;

            float collisionLength = _laserConfig.CollisionLength;
            collisionTargetComponent.Target = ObjectType.Enemy;
            objectTypeComponent.ObjectType = ObjectType.Bullet;
            
            _laserDataViewService.CreateView(id, _laserConfig);
            _laserDataViewService.SetPosition(id, position);
            _laserDataViewService.SetRotation(id, rotation);
            _laserDataViewService.SetLength(id, collisionLength);
        }
    }
}