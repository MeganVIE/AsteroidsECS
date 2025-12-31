using Leopotam.EcsProto;

namespace Common
{
    public abstract class AbstractAspect<T> : IProtoAspect where T : struct
    {
        public ProtoPool<T> Pool;
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