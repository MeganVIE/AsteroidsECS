using CameraData.Components;
using Leopotam.EcsProto;

namespace CameraData.Aspects
{
    public class CameraDataAspect : IProtoAspect
    {
        public ProtoPool<CameraDataComponent> Pool;
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