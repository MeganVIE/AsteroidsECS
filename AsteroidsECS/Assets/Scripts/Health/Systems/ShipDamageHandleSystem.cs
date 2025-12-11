using EntityTags.Components;
using Health.Aspects;
using Health.Components;
using Leopotam.EcsProto;
using UI.Services;
using Utils;

namespace Health.Systems
{
    public class ShipDamageHandleSystem : IProtoRunSystem, IProtoInitSystem
    {
        HealthAspect _healthAspect;
        IGameOverService _gameOverService;
        
        ProtoIt _it;

        ProtoWorld _world;
        
        public void Init(IProtoSystems systems)
        {
            _world = systems.World();
            _healthAspect = _world.GetAspect<HealthAspect>();
            _gameOverService = systems.GetService<IGameOverService>();

            _it = new(new[] { typeof(HealthComponent), typeof(ShipComponent) });
            _it.Init(_world);
        }
        
        public void Run()
        {
            foreach (var entity in _it)
            {
                HealthComponent healthComponent = _healthAspect.Pool.Get(entity);

                if (healthComponent.Value <= 0)
                {
                    _gameOverService.GameOver();
                }
            }
        }
    }
}