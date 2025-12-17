using Destroy.Aspects;
using Health.Aspects;
using Health.Components;
using Leopotam.EcsProto;
using Utils;

namespace Health.Systems
{
    public class DamageHandleSystem : IProtoRunSystem, IProtoInitSystem
    {
        private HealthAspect _healthAspect;
        private DestroyAspect _destroyAspect;
        
        private ProtoIt _it;

        public void Init(IProtoSystems systems)
        {
            ProtoWorld world = systems.World();

            _healthAspect = world.GetAspect<HealthAspect>();
            _destroyAspect = world.GetAspect<DestroyAspect>();

            _it = new(new[] { typeof(HealthComponent)});
            _it.Init(world);
        }
        
        public void Run()
        {
            foreach (var entity in _it)
            {
                HealthComponent healthComponent = _healthAspect.Pool.Get(entity);

                if (healthComponent.Value <= 0)
                {
                    _destroyAspect.Pool.Add(entity);
                }
            }
        }
    }
}