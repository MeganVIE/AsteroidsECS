using System;
using Destroy.Systems;
using Leopotam.EcsProto;
using Moving.Aspects;
using Moving.Systems;

namespace Moving
{
    public class MovingSystemsModule : IProtoModule
    {
        public void Init(IProtoSystems systems)
        {
            systems
                .AddSystem(new RotationByInputSystem())
                .AddSystem(new MoveAccelerationSpeedChangeByInputSystem())
                .AddSystem(new MoveSlowdownSpeedChangeByInputSystem())
                .AddSystem(new MoveSpeedLimitSystem())
                .AddSystem(new MoveSystem())
                .AddSystem(new ShipFollowSystem())
                .AddSystem(new DestroyOutsideScreenSystem())
                .AddSystem(new TeleportWhenOutsideScreenSystem());
        }

        public IProtoAspect[] Aspects()
        {
            return new IProtoAspect[]
            {
                new MovableAspect(), new MoveSpeedAspect(), new AccelerationSpeedAspect(), new MoveSpeedLimitAspect(),
                new RotationAspect(), new SlowdownSpeedAspect()
            };
        }

        public Type[] Dependencies()
        {
            return null;
        }
    }
}