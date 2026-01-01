using Destroy.Components;
using Leopotam.EcsProto;
using Ship.Components;
using Ship.Services;
using Utils;

namespace Ship.Systems
{
    public class ShipDestroySystem : IProtoRunSystem, IProtoInitSystem
    {
        private IShipDataViewService _shipDataViewService;
        
        private ProtoIt _it;
        private ProtoWorld _world;
        
        public void Init(IProtoSystems systems)
        {
            _world = systems.World();
            _shipDataViewService = systems.GetService<IShipDataViewService>();

            _it = new(new[] { typeof(DestroyComponent), typeof(ShipComponent) });
            _it.Init(_world);
        }
        
        public void Run()
        {
            foreach (var entity in _it)
            {
                _shipDataViewService.Destroy();
                _world.DelEntity(entity);
            }
        }
    }
}