using EntityTags.Aspects;
using EntityTags.Components;
using Leopotam.EcsProto;
using Moving.Aspects;
using Moving.Components;
using UI.Services;
using Utils;

namespace UI.Systems
{
    public class ObjectIdViewSystem<TService, TComponent> : IProtoRunSystem, IProtoInitSystem
        where TService : class, IViewPositionService
        where TComponent : struct
    {
        private ObjectIdAspect _objectIdAspect;
        private MovableAspect _movableAspect;
        private ProtoIt _it;
        
        private TService _service;

        public void Init(IProtoSystems systems)
        {
            ProtoWorld world = systems.World();
            _service = systems.GetService<TService>();

            _objectIdAspect = world.GetAspect<ObjectIdAspect>();
            _movableAspect = world.GetAspect<MovableAspect>();
            
            _it = new(new[] { typeof(TComponent), typeof(MovableComponent), typeof(ObjectIDComponent) });
            _it.Init(world);
        }
        
        public void Run()
        {
            foreach (ProtoEntity entity in _it) 
            {
                MovableComponent movableComponent = _movableAspect.Pool.Get(entity);
                ObjectIDComponent objectIDComponent = _objectIdAspect.Pool.Get(entity);

                _service.SetPosition(objectIDComponent.Id, movableComponent.Position);
            }
        }
    }
}