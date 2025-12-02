using System;
using Components;
using Leopotam.EcsProto;
using Moving.Aspects;
using Utils;

namespace Moving.Systems
{
    public class MoveSpeedLimitSystem : IProtoInitSystem, IProtoRunSystem
    {
        MoveSpeedAspect _moveSpeedAspect;
        MoveSpeedLimitAspect _moveSpeedLimitAspect;
        ProtoIt _it;
        
        public void Init(IProtoSystems systems)
        {
            ProtoWorld world = systems.World();
            
            _moveSpeedAspect = world.GetAspect<MoveSpeedAspect>();
            _moveSpeedLimitAspect = world.GetAspect<MoveSpeedLimitAspect>();

            _it = new(new[] { typeof(MoveSpeedComponent), typeof(MoveSpeedLimitComponent) });
            _it.Init(world);
        }

        public void Run()
        {
            foreach (ProtoEntity entity in _it) 
            {
                ref MoveSpeedComponent moveSpeedComponent = ref _moveSpeedAspect.Pool.Get(entity);
                MoveSpeedLimitComponent moveSpeedLimitComponent = _moveSpeedLimitAspect.Pool.Get(entity);

                moveSpeedComponent.Value = Math.Min(moveSpeedComponent.Value, moveSpeedLimitComponent.Value);
            }
        }
    }
}