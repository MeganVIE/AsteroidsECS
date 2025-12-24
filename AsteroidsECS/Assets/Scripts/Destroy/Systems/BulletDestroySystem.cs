using Destroy.Components;
using EntityTags.Aspects;
using EntityTags.Components;
using Leopotam.EcsProto;
using UI.Services;
using Utils;

namespace Destroy.Systems
{
    public class BulletDestroySystem : IProtoRunSystem, IProtoInitSystem
    {
        private BulletAspect _bulletAspect;
        private IBulletDataViewService _bulletDataViewService;
        
        private ProtoIt _it;
        private ProtoWorld _world;

        public void Init(IProtoSystems systems)
        {
            _world = systems.World();

            _bulletAspect = _world.GetAspect<BulletAspect>();
            _bulletDataViewService = systems.GetService<IBulletDataViewService>();

            _it = new(new[] { typeof(DestroyComponent), typeof(BulletComponent) });
            _it.Init(_world);
        }
        
        public void Run()
        {
            foreach (var entity in _it)
            {
                BulletComponent bulletComponent = _bulletAspect.Pool.Get(entity);
                    
                _world.DelEntity(entity);
                _bulletDataViewService.Destroy(bulletComponent.Id);
            }
        }
    }
}