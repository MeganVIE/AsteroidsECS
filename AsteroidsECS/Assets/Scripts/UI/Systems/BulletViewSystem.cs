using EntityTags.Aspects;
using EntityTags.Components;
using Leopotam.EcsProto;
using Moving.Aspects;
using Moving.Components;
using UI.Services;
using Utils;

namespace UI.Systems
{
    public class BulletViewSystem : IProtoRunSystem, IProtoInitSystem
    {
        private BulletAspect _bulletAspect;
        private MovableAspect _movableAspect;
        private ProtoIt _it;
        
        private IBulletDataViewService _bulletDataViewService;

        public void Init(IProtoSystems systems)
        {
            ProtoWorld world = systems.World();
            _bulletDataViewService = systems.GetService<IBulletDataViewService>();

            _bulletAspect = world.GetAspect<BulletAspect>();
            _movableAspect = world.GetAspect<MovableAspect>();
            
            _it = new(new[] { typeof(BulletComponent), typeof(MovableComponent) });
            _it.Init(world);
        }
        
        public void Run()
        {
            foreach (ProtoEntity entity in _it) 
            {
                MovableComponent movableComponent = _movableAspect.Pool.Get(entity);
                BulletComponent bulletComponent = _bulletAspect.Pool.Get(entity);

                _bulletDataViewService.SetPosition(bulletComponent.Id, movableComponent.Position);
            }
        }
    }
}