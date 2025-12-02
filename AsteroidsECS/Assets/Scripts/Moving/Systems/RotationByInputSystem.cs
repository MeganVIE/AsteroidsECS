using Inputs.Aspects;
using Inputs.Components;
using Leopotam.EcsProto;
using Moving.Aspects;
using Moving.Components;
using Utils;

namespace Moving.Systems
{
    public class RotationByInputSystem : IProtoInitSystem, IProtoRunSystem
    {
        RotationAspect _rotationAspect;
        MoveInputEventAspect _moveInputEventAspect;
        ProtoIt _it;

        private IDeltaTimeService _deltaTimeService;

        public void Init(IProtoSystems systems)
        {
            ProtoWorld world = systems.World();
            _deltaTimeService = systems.GetService<IDeltaTimeService>();
            
            _rotationAspect = world.GetAspect<RotationAspect>();
            _moveInputEventAspect = world.GetAspect<MoveInputEventAspect>();
            
            _it = new(new[] { typeof(RotationComponent), typeof(MoveInputEventComponent) });
            _it.Init(world);
        }
        
        public void Run()
        { 
            foreach (ProtoEntity entity in _it) 
            {
                ref RotationComponent rotationComponent = ref _rotationAspect.Pool.Get(entity);
                MoveInputEventComponent moveInputEventComponent = _moveInputEventAspect.Pool.Get(entity);

                var angle = rotationComponent.Angle + moveInputEventComponent.RotationValue * _deltaTimeService.DeltaTime * rotationComponent.RotationSpeed;
                rotationComponent.Angle = angle % 360;
            }
        }
    }
}