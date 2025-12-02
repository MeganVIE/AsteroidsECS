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
        CameraDataAspect _cameraDataAspect;
        
        ProtoIt _movableIt;
        ProtoIt _cameraDataIt;

        public void Init(IProtoSystems systems)
        {
            ProtoWorld world = systems.World();
            
            _movableAspect = world.GetAspect<MovableAspect>();
            _cameraDataAspect = world.GetAspect<CameraDataAspect>();

            _movableIt = new(new[] { typeof(MovableComponent) });
            _movableIt.Init(world);

            _cameraDataIt = new(new[] { typeof(CameraDataComponent) });
            _cameraDataIt.Init(world);
        }
        
        public void Run()
        {
            foreach (ProtoEntity cameraEntity in _cameraDataIt)
            {
                CameraDataComponent cameraDataComponent = _cameraDataAspect.Pool.Get(cameraEntity);
            
                foreach (ProtoEntity entity in _movableIt) 
                {
                    ref MovableComponent movableComponent = ref _movableAspect.Pool.Get(entity);
                    
                    RepeatInViewportVectorComponent(ref movableComponent.Position.X, cameraDataComponent.HalfViewportWidth);
                    RepeatInViewportVectorComponent(ref movableComponent.Position.Y, cameraDataComponent.HalfViewportHeight);
                }
            }
        }

        private void RepeatInViewportVectorComponent(ref float component, float limit)
        {
            component = component > limit ? -limit : component < -limit ? limit : component;
        }
    }
}