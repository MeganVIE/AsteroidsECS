using CameraData.Aspects;
using CameraData.Components;
using Configs;
using Data;
using Leopotam.EcsProto;
using Moving.Aspects;
using Spawn.Systems;
using UFO.Aspects;
using UFO.Components;
using UFO.Services;
using Utils;

namespace UFO.Systems
{
    public class UFOSpawnSystem : EnemySpawnSystem<UFOComponent, UFOAspect, IUFODataViewService, UFOConfig>
    {
        private ShipFollowAspect _shipFollowAspect;
        
        private CameraDataComponent _cameraDataComponent;
        
        private IDeltaTimeService _deltaTimeService;
        private IRandomService _randomService;

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
            _randomService = systems.GetService<IRandomService>();
            
            Config = UFOConfig.LoadFromAssets();
            _respawnTime = Config.DelaySpawnTime;
            
            var world = systems.World();
            _shipFollowAspect = world.GetAspect<ShipFollowAspect>();

            var cameraDataAspect = world.GetAspect<CameraDataAspect>();
            ProtoIt cameraDataIt = new(new[] { typeof(CameraDataComponent) });
            cameraDataIt.Init(world);
            
            foreach (ProtoEntity cameraEntity in cameraDataIt)
            {
                _cameraDataComponent = cameraDataAspect.Pool.Get(cameraEntity);
            }
        }

        protected override void OnSpawn(ProtoEntity entity)
        {
            _shipFollowAspect.Pool.Add(entity);
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
            return _randomService.GetRandom(0, 2) == 0;
        }
    
        private void SetVectorComponentsValue(out float componentA, out float componentB, float limitA, float limitB)
        {
            const float borderThreshold = .02f;
            componentA = GetRandomFiftyFifty() ? limitA - borderThreshold : -limitA + borderThreshold;
            componentB = _randomService.GetRandom(-limitB + borderThreshold, limitB - borderThreshold);
        }
    }
}