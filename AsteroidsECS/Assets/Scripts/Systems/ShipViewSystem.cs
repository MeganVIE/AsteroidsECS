using Aspects;
using Components;
using Leopotam.EcsProto;

namespace Systems
{
    public class ShipViewSystem: IProtoRunSystem, IProtoInitSystem
    {
        MovableAspect _movableAspect;
        ProtoIt _it;
        
        private IShipDataViewProvider _shipDataViewProvider;

        public ShipViewSystem(IShipDataViewProvider shipDataViewProvider)
        {
            _shipDataViewProvider = shipDataViewProvider;
        }

        public void Init(IProtoSystems systems)
        {
            ProtoWorld world = systems.World();
            
            _movableAspect = (MovableAspect)world.Aspect(typeof(MovableAspect));
            
            _it = new(new[] { typeof(ShipComponent), typeof(MovableComponent) });
            _it.Init(world);
        }
        
        public void Run()
        {
            foreach (ProtoEntity entity in _it) 
            {
                MovableComponent movableComponent = _movableAspect.MovableComponentPool.Get(entity);

                _shipDataViewProvider.SetShipPosition(movableComponent.Position);
            }
        }
    }
}