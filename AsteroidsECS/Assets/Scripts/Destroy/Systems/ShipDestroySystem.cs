using Destroy.Components;
using EntityTags.Components;
using Leopotam.EcsProto;
using UI.Services;
using Utils;

namespace Destroy.Systems
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