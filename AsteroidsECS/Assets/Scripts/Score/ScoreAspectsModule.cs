using System.Collections.Generic;
using Leopotam.EcsProto;
using Score.Aspects;

namespace Score
{
    public class ScoreAspectsModule : IProtoAspect
    {
        private List<IProtoAspect> _aspects;

        private ProtoWorld _world;
        
        public void Init(ProtoWorld world)
        {
            _world = world;
            _aspects = new List<IProtoAspect>();
            
            _aspects.Add(new ScoreAspect());
            _aspects.Add(new ScoreChangeAtDeathAspect());
            _aspects.Add(new ScoreIncreaseAspect());
            
            _aspects.ForEach(a => a.Init(_world));
        }

        public void PostInit()
        {
            _aspects.ForEach(a => a.PostInit());
        }

        public ProtoWorld World()
        {
            return _world;
        }
    }
}