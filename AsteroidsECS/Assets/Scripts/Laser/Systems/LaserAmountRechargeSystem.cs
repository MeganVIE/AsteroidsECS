using Laser.Aspects;
using Laser.Components;
using Leopotam.EcsProto;
using Utils;

namespace Laser.Systems
{
    public class LaserAmountRechargeSystem : IProtoInitSystem, IProtoRunSystem
    {
        private LaserAmountAspect _laserAmountAspect;
        private LaserAmountLimitAspect _laserAmountLimitAspect;
        private LaserAmountRechargeAspect _laserAmountRechargeAspect;
        private IDeltaTimeService _deltaTimeService;
        
        private ProtoIt _it;
        
        private float _time;
        
        public void Init(IProtoSystems systems)
        {
            var world = systems.World();

            _laserAmountAspect = world.GetAspect<LaserAmountAspect>();
            _laserAmountLimitAspect = world.GetAspect<LaserAmountLimitAspect>();
            _laserAmountRechargeAspect = world.GetAspect<LaserAmountRechargeAspect>();
            _deltaTimeService = systems.GetService<IDeltaTimeService>();
            

            _it = new(new[] { typeof(LaserAmountRechargeComponent), typeof(LaserAmountComponent), typeof(LaserAmountLimitComponent) });
            _it.Init(world);
        }

        public void Run()
        {
            foreach (var entity in _it)
            {
                _time += _deltaTimeService.DeltaTime;

                LaserAmountRechargeComponent laserAmountRechargeComponent = _laserAmountRechargeAspect.Pool.Get(entity);

                if (laserAmountRechargeComponent.RechargeTime > _time) 
                    return;

                ref LaserAmountComponent laserAmountComponent = ref _laserAmountAspect.Pool.Get(entity);
                LaserAmountLimitComponent laserAmountLimitComponent = _laserAmountLimitAspect.Pool.Get(entity);

                if (laserAmountLimitComponent.Value > laserAmountComponent.Value)
                    laserAmountComponent.Value++;
                
                _time = 0;
            }
        }
    }
}