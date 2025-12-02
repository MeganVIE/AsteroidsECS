using Inputs.Components;
using Leopotam.EcsProto;

namespace Inputs.Aspects
{
    public class MoveInputEventAspect : IProtoAspect
    {
        public ProtoPool<MoveInputEventComponent> Pool;
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