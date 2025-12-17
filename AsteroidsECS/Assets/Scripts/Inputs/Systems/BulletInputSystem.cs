using Inputs.Aspects;
using Inputs.Components;
using Inputs.Services;
using Leopotam.EcsProto;
using Utils;

namespace Inputs.Systems
{
    public class BulletInputSystem : IProtoInitSystem, IProtoRunSystem
    {
        IInputService _inputService;
        BulletInputEventAspect _aspect;
        ProtoIt _it;

        public void Init(IProtoSystems systems)
        {
            ProtoWorld world = systems.World();
            
            _inputService = systems.GetService<IInputService>();
            _aspect = world.GetAspect<BulletInputEventAspect>();
            
            _it = new(new[] { typeof(BulletInputEventComponent) });
            _it.Init(world);
        }
        
        public void Run()
        { 
            foreach (ProtoEntity entity in _it) 
            {
                ref BulletInputEventComponent component = ref _aspect.Pool.Get(entity);

                component.IsGunPressing = _inputService.GunUsing;
            }
        }
    }
}