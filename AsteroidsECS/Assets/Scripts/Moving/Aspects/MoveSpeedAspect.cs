using Components;
using Leopotam.EcsProto;

namespace Moving.Aspects
{
    public class MoveSpeedAspect : IProtoAspect
    {
        public ProtoPool<MoveSpeedComponent> Pool;
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