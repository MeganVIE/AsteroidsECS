using Components;
using Inputs.Aspects;
using Inputs.Components;
using Leopotam.EcsProto;
using Moving.Aspects;
using Moving.Components;
using Utils;

namespace Moving.Systems
{
    public class MoveAccelerationSpeedChangeByInputSystem : IProtoInitSystem, IProtoRunSystem
    {
        MoveInputEventAspect _moveInputEventAspect;
        MoveSpeedAspect _moveSpeedAspect;
        AccelerationSpeedAspect _accelerationSpeedAspect;
        ProtoIt _it;

        private IDeltaTimeService _deltaTimeService;

        public void Init(IProtoSystems systems)
        {
            ProtoWorld world = systems.World();
            _deltaTimeService = systems.GetService<IDeltaTimeService>();
            
            _moveInputEventAspect = world.GetAspect<MoveInputEventAspect>();
            _moveSpeedAspect = world.GetAspect<MoveSpeedAspect>();
            _accelerationSpeedAspect = world.GetAspect<AccelerationSpeedAspect>();
            
            _it = new(new[] { typeof(MoveInputEventComponent), typeof(MoveSpeedComponent), typeof(AccelerationSpeedComponent) });
            _it.Init(world);
        }
        
        public void Run()
        { 
            foreach (ProtoEntity entity in _it) 
            {
                ref MoveSpeedComponent moveSpeedComponent = ref _moveSpeedAspect.Pool.Get(entity);
                MoveInputEventComponent moveInputEventComponent = _moveInputEventAspect.Pool.Get(entity);
                AccelerationSpeedComponent accelerationSpeedComponent = _accelerationSpeedAspect.Pool.Get(entity);

                if (moveInputEventComponent.IsMovePressing)
                {
                    moveSpeedComponent.Value += accelerationSpeedComponent.AccelerationSpeed * _deltaTimeService.DeltaTime;
                }
            }
        }
    }
}