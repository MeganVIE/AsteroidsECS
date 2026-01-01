using Asteroids.Aspects;
using Asteroids.Components;
using Asteroids.Services;
using CameraData.Aspects;
using CameraData.Components;
using Configs;
using Data;
using Leopotam.EcsProto;
using Spawn.Systems;
using Utils;

namespace Asteroids.Systems
{
    public class AsteroidSpawnSystem : EnemySpawnSystem<AsteroidComponent, AsteroidAspect, IAsteroidDataViewService, AsteroidConfig>
    {
        private CameraDataComponent _cameraDataComponent;
        
        private IDeltaTimeService _deltaTimeService;

        private float _respawnTime;
        private float _time;
        
        public override void Run()
        {
            _time += _deltaTimeService.DeltaTime;

            if (_respawnTime > _time) 
                return;
            
            Spawn();
            _time = 0;
        }

        protected override void PostInit(IProtoSystems systems)
        {
            _deltaTimeService = systems.GetService<IDeltaTimeService>();
            
            Config = AsteroidConfig.LoadFromAssets();
            _respawnTime = Config.DelaySpawnTime;
            
            var world = systems.World();
            var cameraDataAspect = world.GetAspect<CameraDataAspect>();
            ProtoIt cameraDataIt = new(new[] { typeof(CameraDataComponent) });
            cameraDataIt.Init(world);
            
            foreach (ProtoEntity cameraEntity in cameraDataIt)
            {
                _cameraDataComponent = cameraDataAspect.Pool.Get(cameraEntity);
            }

            Spawn();
        }

        private void Spawn()
        {
            var position = GetRandomPositionOnBound();
            Spawn(position);
        }

        private Point GetRandomPositionOnBound()
        {
            Point position = new Point();
            if (GetRandomFiftyFifty())
                SetVectorComponentsValue(out position.X, out position.Y, 
                    _cameraDataComponent.HalfViewportWidth, _cameraDataComponent.HalfViewportHeight);
            else
                SetVectorComponentsValue(out position.Y, out position.X, 
                    _cameraDataComponent.HalfViewportHeight, _cameraDataComponent.HalfViewportWidth);
            return position;
        }

        private bool GetRandomFiftyFifty()
        {
            return RandomService.GetRandom(0, 2) == 0;
        }
    
        private void SetVectorComponentsValue(out float componentA, out float componentB, float limitA, float limitB)
        {
            componentA = GetRandomFiftyFifty() ? limitA : -limitA;
            componentB = RandomService.GetRandom(-limitB, limitB);
        }
    }
}