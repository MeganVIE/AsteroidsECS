using Inputs.Aspects;
using Inputs.Components;
using Inputs.Services;
using Leopotam.EcsProto;
using Utils;

namespace Inputs.Systems
{
    public class RotationInputSystem : IProtoInitSystem, IProtoRunSystem
    {
        IInputService _inputService;
        RotationInputEventAspect _aspect;
        ProtoIt _it;

        public void Init(IProtoSystems systems)
        {
            ProtoWorld world = systems.World();
            
            _inputService = systems.GetService<IInputService>();
            _aspect = world.GetAspect<RotationInputEventAspect>();
            
            _it = new(new[] { typeof(RotationInputEventComponent) });
            _it.Init(world);
        }
        
        public void Run()
        { 
            foreach (ProtoEntity entity in _it) 
            {
                ref RotationInputEventComponent component = ref _aspect.Pool.Get(entity);

                component.RotationValue = _inputService.RotationValue;
            }
        }
    }
}