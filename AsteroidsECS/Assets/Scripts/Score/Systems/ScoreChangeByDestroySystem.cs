using Destroy.Components;
using Leopotam.EcsProto;
using Score.Aspects;
using Score.Components;
using Utils;

namespace Score.Systems
{
    public class ScoreChangeByDestroySystem : IProtoInitSystem, IProtoRunSystem
    {
        private ScoreChangeByDestroyAspect _scoreChangeByDestroyAspect;
        private ScoreIncreaseAspect _scoreIncreaseAspect;
        private ProtoIt _it;
        
        public void Init(IProtoSystems systems)
        {
            var world = systems.World();

            _scoreChangeByDestroyAspect = world.GetAspect<ScoreChangeByDestroyAspect>();
            _scoreIncreaseAspect = world.GetAspect<ScoreIncreaseAspect>();

            _it = new(new[] { typeof(ScoreChangeByDestroyComponent), typeof(DestroyComponent) });
            _it.Init(world);
        }

        public void Run()
        {
            foreach (var entity in _it)
            {
                ScoreChangeByDestroyComponent scoreChangeByDestroyComponent = _scoreChangeByDestroyAspect.Pool.Get(entity);
                ref ScoreIncreaseComponent scoreIncreaseComponent = ref _scoreIncreaseAspect.Pool.NewEntity(out _);
                scoreIncreaseComponent.Amount = scoreChangeByDestroyComponent.Amount;
            }
        }
    }
}