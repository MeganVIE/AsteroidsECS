using System;
using Leopotam.EcsProto;
using Moving.Aspects;
using Moving.Systems;
using Systems;

namespace Moving
{
    public class MovingSystemsModule : IProtoModule
    {
        public void Init(IProtoSystems systems)
        {
            systems
                .AddSystem(new RotationByInputSystem())
                .AddSystem(new MoveSpeedChangeByInputSystem())
                .AddSystem(new MoveSpeedLimitSystem())
                .AddSystem(new MoveSystem());
        }

        public IProtoAspect[] Aspects()
        {
            return new IProtoAspect[]
            {
                new MovableAspect(), new MoveSpeedAspect(), new MoveSpeedChangeAspect(), new MoveSpeedLimitAspect(),
                new RotationAspect()
            };
        }

        public Type[] Dependencies()
        {
            return null;
        }
    }
}