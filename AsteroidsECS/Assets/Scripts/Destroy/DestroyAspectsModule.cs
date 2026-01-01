using System.Collections.Generic;
using Destroy.Aspects;
using Leopotam.EcsProto;

namespace Destroy
{
    public class DestroyAspectsModule : IProtoAspect
    {
        private List<IProtoAspect> _aspects;

        private ProtoWorld _world;
        
        public void Init(ProtoWorld world)
        {
            _world = world;
            _aspects = new List<IProtoAspect>();
            
            _aspects.Add(new DestroyAspect());
            _aspects.Add(new DestroyByTimerAspect());
            _aspects.Add(new DestroyOutsideScreenAspect());
            
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