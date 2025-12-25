using System;
using EntityTags.Components;
using Leopotam.EcsProto;
using Moving.Aspects;
using Moving.Components;
using Utils;

namespace Moving.Systems
{
    public class ShipFollowSystem : IProtoRunSystem, IProtoInitSystem
    {
        private MovableAspect _movableAspect;
        private RotationAspect _rotationAspect;

        private ProtoIt _shipIt;
        private ProtoIt _it;

        public void Init(IProtoSystems systems)
        {
            ProtoWorld world = systems.World();
            
            _movableAspect = world.GetAspect<MovableAspect>();
            _rotationAspect = world.GetAspect<RotationAspect>();

            _shipIt = new(new[] { typeof(ShipComponent), typeof(MovableComponent) });
            _shipIt.Init(world);
            
            _it = new(new[] { typeof(ShipFollowComponent), typeof(MovableComponent), typeof(RotationComponent) });
            _it.Init(world);
        }
        
        public void Run()
        {
            foreach (var shipEntity in _shipIt)
            {
                MovableComponent shipMovableComponent = _movableAspect.Pool.Get(shipEntity);
                
                foreach (ProtoEntity entity in _it)
                {
                    MovableComponent movableComponent = _movableAspect.Pool.Get(entity);
                    ref RotationComponent rotationComponent = ref _rotationAspect.Pool.Get(entity);

                    var vector = shipMovableComponent.Position - movableComponent.Position;
                    var cos = vector.Y / Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
                    float angle = (float)(Math.Acos(cos) * 180 / Math.PI);
                    rotationComponent.Angle = vector.X < 0 ? angle : -angle;
                }
            }
        }
    }
}