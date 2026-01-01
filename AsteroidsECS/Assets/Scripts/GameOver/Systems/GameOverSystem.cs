using GameOver.Services;
using Leopotam.EcsProto;
using Score.Aspects;
using Score.Components;
using Ship.Components;
using Utils;

namespace GameOver.Systems
{
    public class GameOverSystem : IProtoInitSystem, IProtoRunSystem
    {
        private ScoreAspect _scoreAspect;
        private IGameOverService _gameOverService;
        
        private ProtoIt _shipIt;
        private ProtoIt _scoreIt;
        
        public void Init(IProtoSystems systems)
        {
            var world = systems.World();
            _scoreAspect = world.GetAspect<ScoreAspect>();
            _gameOverService = systems.GetService<IGameOverService>();
            
            _shipIt = new(new[] { typeof(ShipComponent) });
            _shipIt.Init(world);

            _scoreIt = new(new[] { typeof(ScoreComponent) });
            _scoreIt.Init(world);
        }

        public void Run()
        {
            if (_shipIt.LenSlow() == 0)
            {
                foreach (var entity in _scoreIt)
                {
                    ScoreComponent component = _scoreAspect.Pool.Get(entity);
                    _gameOverService.GameOver(component.Value);
                }
            }
        }
    }
}