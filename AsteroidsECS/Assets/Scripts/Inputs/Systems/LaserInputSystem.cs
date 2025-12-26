using Inputs.Aspects;
using Inputs.Components;
using Inputs.Services;
using Leopotam.EcsProto;
using Utils;

namespace Inputs.Systems
{
    public class LaserInputSystem : IProtoInitSystem, IProtoRunSystem
    {
        IInputService _inputService;
        LaserInputEventAspect _aspect;
        ProtoIt _it;

        public void Init(IProtoSystems systems)
        {
            ProtoWorld world = systems.World();
            
            _inputService = systems.GetService<IInputService>();
            _aspect = world.GetAspect<LaserInputEventAspect>();
            
            _it = new(new[] { typeof(LaserInputEventComponent) });
            _it.Init(world);
        }
        
        public void Run()
        { 
            foreach (ProtoEntity entity in _it) 
            {
                ref LaserInputEventComponent component = ref _aspect.Pool.Get(entity);

                component.IsLaserPressing = _inputService.LaserUsing;
            }
        }
    }
}