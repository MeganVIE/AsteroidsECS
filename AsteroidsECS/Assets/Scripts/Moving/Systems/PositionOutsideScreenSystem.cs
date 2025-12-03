using Aspects;
using Components;
using Leopotam.EcsProto;
using Moving.Aspects;
using Moving.Components;
using Utils;

namespace Moving.Systems
{
    public class PositionOutsideScreenSystem : IProtoRunSystem, IProtoInitSystem
    {
        MovableAspect _movableAspect;
        
        ProtoIt _movableIt;
        ProtoIt _cameraDataIt;
        
        CameraDataComponent _cameraDataComponent;

        public void Init(IProtoSystems systems)
        {
            ProtoWorld world = systems.World();
            
            _movableAspect = world.GetAspect<MovableAspect>();

            _movableIt = new(new[] { typeof(MovableComponent) });
            _movableIt.Init(world);

            var cameraDataAspect = world.GetAspect<CameraDataAspect>();
            _cameraDataIt = new(new[] { typeof(CameraDataComponent) });
            _cameraDataIt.Init(world);
            
            foreach (ProtoEntity cameraEntity in _cameraDataIt)
            {
                _cameraDataComponent = cameraDataAspect.Pool.Get(cameraEntity);
            }
        }
        
        public void Run()
        {
            foreach (ProtoEntity entity in _movableIt) 
            {
                ref MovableComponent movableComponent = ref _movableAspect.Pool.Get(entity);
                    
                RepeatInViewportVectorComponent(ref movableComponent.Position.X, _cameraDataComponent.HalfViewportWidth);
                RepeatInViewportVectorComponent(ref movableComponent.Position.Y, _cameraDataComponent.HalfViewportHeight);
            }
        }

        private void RepeatInViewportVectorComponent(ref float component, float limit)
        {
            component = component > limit ? -limit : component < -limit ? limit : component;
        }
    }
}