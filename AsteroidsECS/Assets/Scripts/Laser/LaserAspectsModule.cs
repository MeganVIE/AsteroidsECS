using System.Collections.Generic;
using Laser.Aspects;
using Leopotam.EcsProto;

namespace Laser
{
    public class LaserAspectsModule : IProtoAspect
    {
        private List<IProtoAspect> _aspects;

        private ProtoWorld _world;
        
        public void Init(ProtoWorld world)
        {
            _world = world;
            _aspects = new List<IProtoAspect>();
            
            _aspects.Add(new LaserAspect());
            _aspects.Add(new LaserAmountLimitAspect());
            _aspects.Add(new LaserAmountAspect());
            
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