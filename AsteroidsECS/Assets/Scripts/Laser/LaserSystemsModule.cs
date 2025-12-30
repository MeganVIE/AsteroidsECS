using System;
using Inputs;
using Laser.Services;
using Laser.Systems;
using Leopotam.EcsProto;
using Moving;

namespace Laser
{
    public class LaserSystemsModule : IProtoModule
    {
        public void Init(IProtoSystems systems)
        {
            systems
                .AddSystem(new LaserCollisionSystem(), -1)
                .AddSystem(new LaserAmountInitSystem())
                .AddSystem(new LaserSpawnSystem())
                .AddSystem(new LaserAmountRechargeSystem())
                .AddSystem(new LaserInputEventSystem())
                .AddSystem(new LaserDestroySystem(), 200)
                
                .AddService(new LaserDataViewService(), typeof(ILaserDataViewService));
        }

        public IProtoAspect[] Aspects()
        {
            return null;
        }

        public Type[] Dependencies()
        {
            return new [] { typeof(MovingSystemsModule), typeof(InputsSystemsModule) };
        }
    }
}