using Leopotam.EcsProto;
using Score.Aspects;
using Score.Components;
using Utils;

namespace Score.Systems
{
    public class ScoreChangeSystem : IProtoInitSystem, IProtoRunSystem
    {
        private ScoreAspect _scoreAspect;
        private ScoreIncreaseAspect _scoreIncreaseAspect;

        private ProtoIt _scoreIt;
        private ProtoIt _scoreIncreaseIt;
        
        public void Init(IProtoSystems systems)
        {
            ProtoWorld world = systems.World();

            _scoreAspect = world.GetAspect<ScoreAspect>();
            _scoreIncreaseAspect = world.GetAspect<ScoreIncreaseAspect>();
            
            _scoreIt = new(new[] { typeof(ScoreComponent) });
            _scoreIt.Init(world);
            
            _scoreIncreaseIt = new(new[] { typeof(ScoreIncreaseComponent) });
            _scoreIncreaseIt.Init(world);

            _scoreAspect.Pool.NewEntity(out _);
        }

        public void Run()
        {
            foreach (var scoreEntity in _scoreIt)
            {
                ref ScoreComponent scoreComponent = ref _scoreAspect.Pool.Get(scoreEntity);

                foreach (var increaseEntity in _scoreIncreaseIt)
                {
                    ScoreIncreaseComponent scoreIncreaseComponent = _scoreIncreaseAspect.Pool.Get(increaseEntity);
                    scoreComponent.Value += scoreIncreaseComponent.Amount;
                    _scoreIncreaseAspect.Pool.Del(increaseEntity);
                }
            }
        }
    }
}