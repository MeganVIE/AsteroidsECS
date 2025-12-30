using Configs;
using Laser.Aspects;
using Laser.Components;
using Leopotam.EcsProto;
using Utils;

namespace Laser.Systems
{
    public class LaserAmountInitSystem : IProtoInitSystem
    {
        public void Init(IProtoSystems systems)
        {
            var laserConfig = LaserConfig.LoadFromAssets();
            
            var world = systems.World();
            var laserAmountAspect = world.GetAspect<LaserAmountAspect>();
            var laserAmountLimitAspect = world.GetAspect<LaserAmountLimitAspect>();
            var laserAmountRechargeAspect = world.GetAspect<LaserAmountRechargeAspect>();
            
            laserAmountAspect.Pool.NewEntity(out ProtoEntity entity);
            
            ref LaserAmountLimitComponent laserAmountLimitComponent = ref laserAmountLimitAspect.Pool.Add(entity);
            laserAmountLimitComponent.Value = laserConfig.MaxAmount;

            ref LaserAmountRechargeComponent laserAmountRechargeComponent = ref laserAmountRechargeAspect.Pool.Add(entity);
            laserAmountRechargeComponent.RechargeTime = laserConfig.AmountRechargeTime;
        }
    }
}