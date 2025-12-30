using Collisions.Aspects;
using Collisions.Components;
using Components;
using Configs;
using Data;
using EntityTags.Aspects;
using EntityTags.Components;
using Health.Aspects;
using Health.Components;
using Leopotam.EcsProto;
using Moving.Aspects;
using Moving.Components;
using Spawn.Aspects;
using Spawn.Components;
using UI.Services;
using Utils;

namespace Spawn.Systems
{
    public class AsteroidPartSpawnSystem : IProtoInitSystem, IProtoRunSystem
    {
        private AsteroidPartAspect _asteroidPartAspect;
        private ObjectIdAspect _objectIdAspect;
        
        private MovableAspect _movableAspect;
        private RotationAspect _rotationAspect;
        private MoveSpeedAspect _moveSpeedAspect;
        private CollisionRadiusAspect _collisionRadiusAspect;
        private CollisionTargetAspect _collisionTargetAspect;
        private CollisionObjectTypeAspect _collisionObjectTypeAspect;
        private HealthAspect _healthAspect;
        private TeleportOutsideScreenAspect _teleportOutsideScreenAspect;

        private SpawnAsteroidPartAspect _spawnAsteroidPartAspect;
        
        private IAsteroidPartDataViewService _asteroidPartDataViewService;
        private IRandomService _randomService;
        private AsteroidPartConfig _asteroidPartConfig;

        private ProtoIt _it;

        private int _lastId;

        public void Init(IProtoSystems systems)
        {
            _randomService = systems.GetService<IRandomService>();
            _asteroidPartDataViewService = systems.GetService<IAsteroidPartDataViewService>();

            _asteroidPartConfig = AsteroidPartConfig.LoadFromAssets();
            
            var world = systems.World();
            _asteroidPartAspect = world.GetAspect<AsteroidPartAspect>();
            _objectIdAspect = world.GetAspect<ObjectIdAspect>();
            _movableAspect = world.GetAspect<MovableAspect>();
            _rotationAspect = world.GetAspect<RotationAspect>();
            _moveSpeedAspect = world.GetAspect<MoveSpeedAspect>();
            _collisionRadiusAspect = world.GetAspect<CollisionRadiusAspect>();
            _collisionTargetAspect = world.GetAspect<CollisionTargetAspect>();
            _collisionObjectTypeAspect = world.GetAspect<CollisionObjectTypeAspect>();
            _healthAspect = world.GetAspect<HealthAspect>();
            _teleportOutsideScreenAspect = world.GetAspect<TeleportOutsideScreenAspect>();
            _spawnAsteroidPartAspect = world.GetAspect<SpawnAsteroidPartAspect>();

            _it = new ProtoIt(new[] { typeof(SpawnAsteroidPartComponent) });
            _it.Init(world);
        }
        
        public void Run()
        {
            foreach (var entity in _it)
            {
                SpawnAsteroidPartComponent component = _spawnAsteroidPartAspect.Pool.Get(entity);

                for (int i = 0; i < _asteroidPartConfig.SpawnAmount; i++)
                {
                    Spawn(component.SpawnPosition);
                }
                
                _spawnAsteroidPartAspect.Pool.Del(entity);
            }
        }

        private void Spawn(Point position)
        {
            _asteroidPartAspect.Pool.NewEntity(out ProtoEntity entity);
            _teleportOutsideScreenAspect.Pool.Add(entity);

            ref ObjectIDComponent objectIDComponent = ref _objectIdAspect.Pool.Add(entity);
            
            ref MoveSpeedComponent moveSpeedComponent = ref _moveSpeedAspect.Pool.Add(entity);
            ref MovableComponent movableComponent = ref _movableAspect.Pool.Add(entity);
            ref RotationComponent rotationComponent = ref _rotationAspect.Pool.Add(entity);

            ref CollisionRadiusComponent collisionRadiusComponent = ref _collisionRadiusAspect.Pool.Add(entity);
            ref CollisionTargetComponent collisionTargetComponent = ref _collisionTargetAspect.Pool.Add(entity);
            ref CollisionObjectTypeComponent collisionObjectTypeComponent = ref _collisionObjectTypeAspect.Pool.Add(entity);
            ref HealthComponent healthComponent = ref _healthAspect.Pool.Add(entity);

            int id = ++_lastId;
            objectIDComponent.Id = id;
            
            movableComponent.Position = position;
            rotationComponent.Angle = _randomService.GetRandom(0, 360);
            moveSpeedComponent.Value = _asteroidPartConfig.StartMoveSpeed;

            collisionRadiusComponent.Value = _asteroidPartConfig.CollisionRadius;
            collisionTargetComponent.Target = ObjectType.Ship;
            collisionObjectTypeComponent.ObjectType = ObjectType.Enemy;
            healthComponent.Value = 1;
            
            _asteroidPartDataViewService!.CreateView(id, _asteroidPartConfig);
            _asteroidPartDataViewService.SetPosition(id, position);
        }
    }
}