using Components;
using Inputs.Aspects;
using Inputs.Components;
using Leopotam.EcsProto;
using Moving.Aspects;
using Moving.Components;
using Utils;

namespace Moving.Systems
{
    public class MoveSlowdownSpeedChangeByInputSystem : IProtoInitSystem, IProtoRunSystem
    {
        MoveInputEventAspect _moveInputEventAspect;
        MoveSpeedAspect _moveSpeedAspect;
        SlowdownSpeedAspect _slowdownSpeedAspect;
        ProtoIt _it;

        private IDeltaTimeService _deltaTimeService;

        public void Init(IProtoSystems systems)
        {
            ProtoWorld world = systems.World();
            _deltaTimeService = systems.GetService<IDeltaTimeService>();
            
            _moveInputEventAspect = world.GetAspect<MoveInputEventAspect>();
            _moveSpeedAspect = world.GetAspect<MoveSpeedAspect>();
            _slowdownSpeedAspect = world.GetAspect<SlowdownSpeedAspect>();
            
            _it = new(new[] { typeof(MoveInputEventComponent), typeof(MoveSpeedComponent), typeof(SlowdownSpeedComponent) });
            _it.Init(world);
        }
        
        public void Run()
        { 
            foreach (ProtoEntity entity in _it) 
            {
                ref MoveSpeedComponent moveSpeedComponent = ref _moveSpeedAspect.Pool.Get(entity);
                MoveInputEventComponent moveInputEventComponent = _moveInputEventAspect.Pool.Get(entity);
                SlowdownSpeedComponent slowdownSpeedComponent = _slowdownSpeedAspect.Pool.Get(entity);

                if (moveInputEventComponent.IsMovePressing) 
                    continue;
                
                moveSpeedComponent.Value -= slowdownSpeedComponent.SlowdownSpeed * _deltaTimeService.DeltaTime;

                if (moveSpeedComponent.Value < 0)
                    moveSpeedComponent.Value = 0;
            }
        }
    }
}