using System;
using Inputs.Aspects;
using Inputs.Services;
using Inputs.Systems;
using Leopotam.EcsProto;

namespace Inputs
{
    public class InputsSystemsModule : IProtoModule
    {
        IInputService _inputService;
        
        public InputsSystemsModule(IInputService inputService)
        {
            _inputService = inputService;
        }
        
        public void Init(IProtoSystems systems)
        {
            systems
                .AddSystem(new MoveInputSystem())
                .AddSystem(new RotationInputSystem())
                .AddSystem(new BulletInputSystem())
                .AddSystem(new LaserInputSystem())
                
                .AddService(_inputService, typeof(IInputService));
        }

        public IProtoAspect[] Aspects()
        {
            return new IProtoAspect[]
            {
                new MoveInputEventAspect(), new RotationInputEventAspect()
            };
        }

        public Type[] Dependencies()
        {
            return null;
        }
    }
}