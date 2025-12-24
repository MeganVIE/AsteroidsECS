using EntityTags.Components;
using Leopotam.EcsProto;
using Moving.Aspects;
using Moving.Components;
using Spawn.Aspects;
using Spawn.Components;
using UI.Services;
using Utils;

namespace Destroy.Systems
{
    public class AsteroidsDestroySystem : DestroyByIdSystem<IAsteroidDataViewService, AsteroidComponent>
    {
        private MovableAspect _movableAspect;
        private SpawnAsteroidPartAspect _spawnAsteroidPartAspect;
        
        protected override void InitialInit()
        {
            _movableAspect = World.GetAspect<MovableAspect>();
            _spawnAsteroidPartAspect = World.GetAspect<SpawnAsteroidPartAspect>();
        }

        protected override void BeforeDestroy(ProtoEntity entity)
        {
            if (_movableAspect.Pool.Has(entity))
            {
                MovableComponent movableComponent = _movableAspect.Pool.Get(entity);
                ref SpawnAsteroidPartComponent spawnAsteroidPartComponent = ref _spawnAsteroidPartAspect.Pool.NewEntity(out _);
                spawnAsteroidPartComponent.SpawnPosition = movableComponent.Position;
            }
        }
    }
}