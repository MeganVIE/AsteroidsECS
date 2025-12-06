using EntityTags.Components;
using Leopotam.EcsProto;

namespace EntityTags.Aspects
{
    public class AsteroidAspect : IProtoAspect
    {
        public ProtoPool<AsteroidComponent> Pool;
        private ProtoWorld _world;
        
        public void Init(ProtoWorld world)
        {
            world.AddAspect (this);
            Pool = new ();
            world.AddPool (Pool);
            _world = world;
        }

        public void PostInit() { }

        public ProtoWorld World()
        {
            return _world;
        }
    }
}