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
        private LaserAmountRechargeTimeAspect _laserAmountRechargeTimeAspect;
        private IDeltaTimeService _deltaTimeService;
        
        private ProtoIt _it;
        
        public void Init(IProtoSystems systems)
        {
            var world = systems.World();

            _laserAmountAspect = world.GetAspect<LaserAmountAspect>();
            _laserAmountLimitAspect = world.GetAspect<LaserAmountLimitAspect>();
            _laserAmountRechargeAspect = world.GetAspect<LaserAmountRechargeAspect>();
            _laserAmountRechargeTimeAspect = world.GetAspect<LaserAmountRechargeTimeAspect>();
            _deltaTimeService = systems.GetService<IDeltaTimeService>();
            

            _it = new(new[] { typeof(LaserAmountRechargeTimeComponent), typeof(LaserAmountRechargeComponent),
                typeof(LaserAmountComponent), typeof(LaserAmountLimitComponent) });
            _it.Init(world);
        }

        public void Run()
        {
            foreach (var entity in _it)
            {
                ref LaserAmountRechargeComponent rechargeComponent = ref _laserAmountRechargeAspect.Pool.Get(entity);
                rechargeComponent.Value -= _deltaTimeService.DeltaTime;

                LaserAmountRechargeTimeComponent laserAmountRechargeTimeComponent = _laserAmountRechargeTimeAspect.Pool.Get(entity);

                if (rechargeComponent.Value > 0)
                    return;

                ref LaserAmountComponent laserAmountComponent = ref _laserAmountAspect.Pool.Get(entity);
                LaserAmountLimitComponent laserAmountLimitComponent = _laserAmountLimitAspect.Pool.Get(entity);

                if (laserAmountLimitComponent.Value > laserAmountComponent.Value)
                    laserAmountComponent.Value++;
                
                rechargeComponent.Value = laserAmountRechargeTimeComponent.RechargeTime;
            }
        }
    }
}