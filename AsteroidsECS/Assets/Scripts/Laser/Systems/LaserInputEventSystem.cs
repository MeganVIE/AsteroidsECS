using Inputs.Aspects;
using Inputs.Components;
using Laser.Aspects;
using Laser.Components;
using Leopotam.EcsProto;
using Moving.Aspects;
using Moving.Components;
using Utils;

namespace Laser.Systems
{
    public class LaserInputEventSystem : IProtoInitSystem, IProtoRunSystem
    {
        private LaserInputEventAspect _laserInputEventAspect;
        private SpawnLaserAspect _spawnLaserAspect;
        private MovableAspect _movableAspect;
        private RotationAspect _rotationAspect;
        
        private ProtoIt _it;
        
        public void Init(IProtoSystems systems)
        {
            var world = systems.World();
            
            _laserInputEventAspect = world.GetAspect<LaserInputEventAspect>();
            _movableAspect = world.GetAspect<MovableAspect>();
            _rotationAspect = world.GetAspect<RotationAspect>();
            _spawnLaserAspect = world.GetAspect<SpawnLaserAspect>();

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

                    ref SpawnLaserComponent component = ref _spawnLaserAspect.Pool.NewEntity(out _);
                    component.Position = movableComponent.Position;
                    component.Rotation = rotationComponent.Angle;
                }
            }
        }
    }
}