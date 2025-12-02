using Leopotam.EcsProto;
using Moving.Components;

namespace Moving.Aspects
{
    public class MoveSpeedChangeAspect : IProtoAspect
    {
        public ProtoPool<MoveSpeedChangeComponent> Pool;
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