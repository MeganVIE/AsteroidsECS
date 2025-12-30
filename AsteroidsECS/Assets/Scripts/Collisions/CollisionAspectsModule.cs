using System.Collections.Generic;
using Collisions.Aspects;
using Leopotam.EcsProto;

namespace Collisions
{
    public class CollisionAspectsModule : IProtoAspect
    {
        private List<IProtoAspect> _aspects;

        private ProtoWorld _world;
        
        public void Init(ProtoWorld world)
        {
            _world = world;
            _aspects = new List<IProtoAspect>();
            
            _aspects.Add(new CollisionLengthAspect());
            _aspects.Add(new CollisionRadiusAspect());
            _aspects.Add(new CollisionTargetAspect());
            _aspects.Add(new CollisionObjectTypeAspect());
            
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