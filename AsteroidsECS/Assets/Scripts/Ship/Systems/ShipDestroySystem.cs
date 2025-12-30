using Destroy.Components;
using Leopotam.EcsProto;
using Ship.Components;
using Ship.Services;
using UI.Services;
using Utils;

namespace Ship.Systems
{
    public class ShipDestroySystem : IProtoRunSystem, IProtoInitSystem
    {
        private IGameOverService _gameOverService;
        private IShipDataViewService _shipDataViewService;
        
        private ProtoIt _it;
        
        public void Init(IProtoSystems systems)
        {
            ProtoWorld world = systems.World();
            
            _gameOverService = systems.GetService<IGameOverService>();
            _shipDataViewService = systems.GetService<IShipDataViewService>();

            _it = new(new[] { typeof(DestroyComponent), typeof(ShipComponent) });
            _it.Init(world);
        }
        
        public void Run()
        {
            foreach (var _ in _it)
            {
                _shipDataViewService.Destroy();
                _gameOverService.GameOver();
            }
        }
    }
}