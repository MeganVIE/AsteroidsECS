using Leopotam.EcsProto;
using UI.Services;
using Utils;

namespace UI.Systems
{
    public abstract class ObjectIdViewClearSystem<TService> : IProtoInitSystem, IProtoDestroySystem
        where TService : class, IViewClearService
    {        
        private TService _service;
        
        public void Init(IProtoSystems systems)
        {
            _service = systems.GetService<TService>();
        }

        public void Destroy()
        {
            _service.Clear();
        }
    }
}