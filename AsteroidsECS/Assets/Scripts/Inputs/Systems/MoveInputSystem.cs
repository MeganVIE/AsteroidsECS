using Inputs.Aspects;
using Inputs.Components;
using Leopotam.EcsProto;
using Utils;

namespace Inputs.Systems
{
    public class MoveInputSystem : IProtoInitSystem, IProtoRunSystem
    {
        IInputService _inputService;
        MoveInputEventAspect _aspect;
        ProtoIt _it;

        public void Init(IProtoSystems systems)
        {
            ProtoWorld world = systems.World();
            
            _inputService = systems.GetService<IInputService>();
            _aspect = world.GetAspect<MoveInputEventAspect>();
            
            _it = new(new[] { typeof(MoveInputEventComponent) });
            _it.Init(world);
        }
        
        public void Run()
        { 
            foreach (ProtoEntity entity in _it) 
            {
                ref MoveInputEventComponent component = ref _aspect.Pool.Get(entity);

                component.IsMovePressing = _inputService.MovePressing;
                component.RotationValue = _inputService.RotationValue;
            }
        }
    }
}