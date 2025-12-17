using Destroy.Components;
using EntityTags.Aspects;
using EntityTags.Components;
using Leopotam.EcsProto;
using UI.Services;
using Utils;

namespace Destroy.Systems
{
    public class AsteroidsDestroySystem : IProtoRunSystem, IProtoInitSystem
    {
        private AsteroidAspect _asteroidAspect;
        private IAsteroidDataViewService _asteroidDataViewService;
        
        private ProtoIt _it;
        private ProtoWorld _world;

        public void Init(IProtoSystems systems)
        {
            _world = systems.World();

            _asteroidAspect = _world.GetAspect<AsteroidAspect>();
            _asteroidDataViewService = systems.GetService<IAsteroidDataViewService>();

            _it = new(new[] { typeof(DestroyComponent), typeof(AsteroidComponent) });
            _it.Init(_world);
        }
        
        public void Run()
        {
            foreach (var entity in _it)
            {
                AsteroidComponent asteroidComponent = _asteroidAspect.Pool.Get(entity);
                    
                _world.DelEntity(entity);
                _asteroidDataViewService.Destroy(asteroidComponent.Id);
            }
        }
    }
}