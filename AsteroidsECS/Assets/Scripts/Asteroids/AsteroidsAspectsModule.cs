using System.Collections.Generic;
using Asteroids.Aspects;
using Leopotam.EcsProto;

namespace Asteroids
{
    public class AsteroidsAspectsModule : IProtoAspect
    {
        private List<IProtoAspect> _aspects;

        private ProtoWorld _world;
        
        public void Init(ProtoWorld world)
        {
            _world = world;
            _aspects = new List<IProtoAspect>();
            
            _aspects.Add(new AsteroidAspect());
            _aspects.Add(new AsteroidPartAspect());
            _aspects.Add(new SpawnAsteroidPartAspect());
            
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