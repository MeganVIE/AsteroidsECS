using Components;
using Leopotam.EcsProto;

namespace Aspects
{
    public class AccelerationAspect : IProtoAspect
    {
        public ProtoPool<AccelerationComponent> AccelerationComponentPool;
        private ProtoWorld _world;
        
        public void Init(ProtoWorld world)
        {
            world.AddAspect (this);
            AccelerationComponentPool = new ();
            world.AddPool (AccelerationComponentPool);
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