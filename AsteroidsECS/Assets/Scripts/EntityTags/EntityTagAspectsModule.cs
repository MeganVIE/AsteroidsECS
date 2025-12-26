using System.Collections.Generic;
using EntityTags.Aspects;
using Leopotam.EcsProto;

namespace EntityTags
{
    public class EntityTagAspectsModule : IProtoAspect
    {
        private List<IProtoAspect> _aspects;

        private ProtoWorld _world;
        
        public void Init(ProtoWorld world)
        {
            _world = world;
            _aspects = new List<IProtoAspect>();
            
            _aspects.Add(new ObjectIdAspect());
            
            _aspects.Add(new ShipAspect());
            _aspects.Add(new BulletAspect());
            _aspects.Add(new LaserAspect());
            
            _aspects.Add(new AsteroidAspect());
            _aspects.Add(new AsteroidPartAspect());
            _aspects.Add(new UFOAspect());
            
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