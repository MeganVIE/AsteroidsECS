using Components;
using Inputs.Aspects;
using Inputs.Components;
using Leopotam.EcsProto;
using Moving.Aspects;
using Moving.Components;

namespace Moving.Systems
{
    public class MoveSpeedChangeByInputSystem : IProtoInitSystem, IProtoRunSystem
    {
        MoveInputEventAspect _moveInputEventAspect;
        MoveSpeedAspect _moveSpeedAspect;
        MoveSpeedChangeAspect _moveSpeedChangeAspect;
        ProtoIt _it;

        private IDeltaTimeService _deltaTimeService;

        public void Init(IProtoSystems systems)
        {
            ProtoWorld world = systems.World();
            _deltaTimeService = systems.GetService<IDeltaTimeService>();
            
            _moveInputEventAspect = world.GetAspect<MoveInputEventAspect>();
            _moveSpeedAspect = world.GetAspect<MoveSpeedAspect>();
            _moveSpeedChangeAspect = world.GetAspect<MoveSpeedChangeAspect>();
            
            _it = new(new[] { typeof(MoveInputEventComponent), typeof(MoveSpeedComponent), typeof(MoveSpeedChangeComponent) });
            _it.Init(world);
        }
        
        public void Run()
        { 
            foreach (ProtoEntity entity in _it) 
            {
                ref MoveSpeedComponent moveSpeedComponent = ref _moveSpeedAspect.Pool.Get(entity);
                MoveInputEventComponent moveInputEventComponent = _moveInputEventAspect.Pool.Get(entity);
                MoveSpeedChangeComponent moveSpeedChangeComponent = _moveSpeedChangeAspect.Pool.Get(entity);

                if (moveInputEventComponent.IsMovePressing)
                {
                    moveSpeedComponent.Value += moveSpeedChangeComponent.AccelerationSpeed * _deltaTimeService.DeltaTime;
                }
                else
                {
                    moveSpeedComponent.Value -= moveSpeedChangeComponent.SlowdownSpeed * _deltaTimeService.DeltaTime;

                    if (moveSpeedComponent.Value < 0)
                        moveSpeedComponent.Value = 0;
                }
            }
        }
    }
}