using Components;
using EntityTags.Aspects;
using EntityTags.Components;
using Leopotam.EcsProto;
using Moving.Aspects;
using Moving.Components;
using UI.Services;
using Utils;

namespace UI.Systems
{
    public class AsteroidViewSystem: IProtoRunSystem, IProtoInitSystem
    {
        private AsteroidAspect _asteroidAspect;
        MovableAspect _movableAspect;
        ProtoIt _it;
        
        private IAsteroidDataViewService _asteroidDataViewService;

        public void Init(IProtoSystems systems)
        {
            ProtoWorld world = systems.World();
            _asteroidDataViewService = systems.GetService<IAsteroidDataViewService>();

            _asteroidAspect = world.GetAspect<AsteroidAspect>();
            _movableAspect = world.GetAspect<MovableAspect>();
            
            _it = new(new[] { typeof(AsteroidComponent), typeof(MovableComponent) });
            _it.Init(world);
        }
        
        public void Run()
        {
            foreach (ProtoEntity entity in _it) 
            {
                MovableComponent movableComponent = _movableAspect.Pool.Get(entity);
                AsteroidComponent asteroidComponent = _asteroidAspect.Pool.Get(entity);

                _asteroidDataViewService.SetPosition(asteroidComponent.Id, movableComponent.Position);
            }
        }
    }
}