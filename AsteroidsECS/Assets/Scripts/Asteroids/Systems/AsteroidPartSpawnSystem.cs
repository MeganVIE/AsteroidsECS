using Asteroids.Aspects;
using Asteroids.Components;
using Asteroids.Services;
using Configs;
using Leopotam.EcsProto;
using Spawn.Systems;
using Utils;

namespace Asteroids.Systems
{
    public class AsteroidPartSpawnSystem : EnemySpawnSystem<AsteroidPartComponent, AsteroidPartAspect, IAsteroidPartDataViewService, AsteroidPartConfig>
    {
        private SpawnAsteroidPartAspect _spawnAsteroidPartAspect;
        private ProtoIt _it;
        
        public override void Run()
        {
            foreach (var entity in _it)
            {
                SpawnAsteroidPartComponent component = _spawnAsteroidPartAspect.Pool.Get(entity);

                for (int i = 0; i < Config.SpawnAmount; i++)
                {
                    Spawn(component.SpawnPosition);
                }
                
                _spawnAsteroidPartAspect.Pool.Del(entity);
            }
        }

        protected override void PostInit(IProtoSystems systems)
        {
            Config = AsteroidPartConfig.LoadFromAssets();
            
            var world = systems.World();
            _spawnAsteroidPartAspect = world.GetAspect<SpawnAsteroidPartAspect>();

            _it = new ProtoIt(new[] { typeof(SpawnAsteroidPartComponent) });
            _it.Init(world);
        }
    }
}