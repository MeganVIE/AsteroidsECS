using EntityTags.Components;
using Leopotam.EcsProto;
using Moving.Aspects;
using Moving.Components;
using UI.Services;
using Utils;

namespace UI.Systems
{
    public class ShipViewSystem : IProtoRunSystem, IProtoInitSystem
    {
        MovableAspect _movableAspect;
        RotationAspect _rotationAspect;
        ProtoIt _it;
        
        private IShipDataViewService _shipDataViewService;

        public void Init(IProtoSystems systems)
        {
            ProtoWorld world = systems.World();
            _shipDataViewService = systems.GetService<IShipDataViewService>();
            
            _movableAspect = world.GetAspect<MovableAspect>();
            _rotationAspect = world.GetAspect<RotationAspect>();
            
            _it = new(new[] { typeof(ShipComponent), typeof(MovableComponent), typeof(RotationComponent) });
            _it.Init(world);
        }
        
        public void Run()
        {
            foreach (ProtoEntity entity in _it) 
            {
                MovableComponent movableComponent = _movableAspect.Pool.Get(entity);
                RotationComponent rotationComponent = _rotationAspect.Pool.Get(entity);

                _shipDataViewService.SetPosition(movableComponent.Position);
                _shipDataViewService.SetShipRotation(rotationComponent.Angle);
            }
        }
    }
}