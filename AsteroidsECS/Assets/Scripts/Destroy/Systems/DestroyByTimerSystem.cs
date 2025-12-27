using Destroy.Aspects;
using Destroy.Components;
using Leopotam.EcsProto;
using Utils;

namespace Destroy.Systems
{
    public class DestroyByTimerSystem : IProtoRunSystem, IProtoInitSystem
    {
        private DestroyByTimerAspect _destroyByTimerAspect;
        private DestroyAspect _destroyAspect;
        private IDeltaTimeService _deltaTimeService;
        
        private ProtoIt _it;

        public void Init(IProtoSystems systems)
        {
            var world = systems.World();

            _destroyByTimerAspect = world.GetAspect<DestroyByTimerAspect>();
            _destroyAspect = world.GetAspect<DestroyAspect>();
            _deltaTimeService = systems.GetService<IDeltaTimeService>();
            
            _it = new(new[] { typeof(DestroyByTimerComponent) });
            _it.Init(world);
        }
        
        public void Run()
        {
            foreach (var entity in _it)
            {
                ref DestroyByTimerComponent component = ref _destroyByTimerAspect.Pool.Get(entity);
                component.LifeTime -= _deltaTimeService.DeltaTime;

                if (component.LifeTime <= 0)
                {
                    if (!_destroyAspect.Pool.Has(entity))
                        _destroyAspect.Pool.Add(entity);
                }
            }
        }
    }
}