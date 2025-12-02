using Components;
using Leopotam.EcsProto;

namespace Moving.Aspects
{
    public class MoveSpeedLimitAspect : IProtoAspect
    {
        public ProtoPool<MoveSpeedLimitComponent> Pool;
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