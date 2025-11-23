using Leopotam.EcsProto;

namespace Aspects
{
    public class MovableAspect : IProtoAspect
    {
        public ProtoPool<MovableComponent> MovableComponentPool;
        private ProtoWorld _world;
        
        public void Init(ProtoWorld world)
        {
            world.AddAspect (this);
            MovableComponentPool = new ();
            world.AddPool (MovableComponentPool);
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