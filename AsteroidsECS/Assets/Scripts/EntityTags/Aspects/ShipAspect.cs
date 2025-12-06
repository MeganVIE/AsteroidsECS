using EntityTags.Components;
using Leopotam.EcsProto;

namespace EntityTags.Aspects
{
    public class ShipAspect : IProtoAspect
    {
        public ProtoPool<ShipComponent> Pool;
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