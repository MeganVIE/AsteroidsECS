using Destroy.Components;
using EntityTags.Aspects;
using EntityTags.Components;
using Leopotam.EcsProto;
using UI.Services;
using Utils;

namespace Destroy.Systems
{
    public abstract class DestroyByIdSystem<TService, TComponent> : IProtoRunSystem, IProtoInitSystem
        where TService : class, IDestroyItemService
        where TComponent : struct
    {
        private ObjectIdAspect _objectIdAspect;
        private TService _service;
        
        private ProtoIt _it;
        private ProtoWorld _world;

        public void Init(IProtoSystems systems)
        {
            _world = systems.World();

            _objectIdAspect = _world.GetAspect<ObjectIdAspect>();
            _service = systems.GetService<TService>();

            _it = new(new[] { typeof(DestroyComponent), typeof(TComponent), typeof(ObjectIDComponent) });
            _it.Init(_world);
        }
        
        public void Run()
        {
            foreach (var entity in _it)
            {
                ObjectIDComponent component = _objectIdAspect.Pool.Get(entity);
                    
                _world.DelEntity(entity);
                _service.Destroy(component.Id);
            }
        }
    }
}