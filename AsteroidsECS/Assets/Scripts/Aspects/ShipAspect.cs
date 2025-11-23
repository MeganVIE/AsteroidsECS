using Components;
using Leopotam.EcsProto;

namespace Aspects
{
    public class ShipAspect : IProtoAspect
    {
        public ProtoPool<ShipComponent> ShipComponentPool;
        private ProtoWorld _world;
        
        public void Init(ProtoWorld world)
        {
            world.AddAspect (this);
            ShipComponentPool = new ();
            world.AddPool (ShipComponentPool);
            _world = world;
        }

        public void PostInit()
        {
        }

        public ProtoWorld World()
        {
            return _world;
        }
    }
}