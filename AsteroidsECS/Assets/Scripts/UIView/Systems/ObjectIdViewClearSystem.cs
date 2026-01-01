using Leopotam.EcsProto;
using UIView.Services;
using Utils;

namespace UIView.Systems
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