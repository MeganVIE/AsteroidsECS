using CameraData.Aspects;
using CameraData.Components;
using Destroy.Aspects;
using Leopotam.EcsProto;
using Moving.Aspects;
using Moving.Components;
using Utils;

namespace Moving.Systems
{
    public class DestroyOutsideScreenSystem : IProtoRunSystem, IProtoInitSystem
    {
        private MovableAspect _movableAspect;
        private DestroyAspect _destroyAspect;
        
        private ProtoIt _it;
        
        private CameraDataComponent _cameraDataComponent;

        public void Init(IProtoSystems systems)
        {
            ProtoWorld world = systems.World();
            
            _destroyAspect = world.GetAspect<DestroyAspect>();
            _movableAspect = world.GetAspect<MovableAspect>();
            
            _it = new(new[] { typeof(MovableComponent), typeof(DestroyOutsideScreenComponent) });
            _it.Init(world);

            var cameraDataAspect = world.GetAspect<CameraDataAspect>();
            ProtoIt cameraDataIt = new(new[] { typeof(CameraDataComponent) });
            cameraDataIt.Init(world);
            
            foreach (ProtoEntity cameraEntity in cameraDataIt)
            {
                _cameraDataComponent = cameraDataAspect.Pool.Get(cameraEntity);
            }
        }
        
        public void Run()
        {
            foreach (ProtoEntity entity in _it) 
            {
                ref MovableComponent movableComponent = ref _movableAspect.Pool.Get(entity);

                if (InViewport(movableComponent.Position.X, _cameraDataComponent.HalfViewportWidth)
                    || InViewport(movableComponent.Position.Y, _cameraDataComponent.HalfViewportHeight))
                {
                    _destroyAspect.Pool.Add(entity);
                }
            }
        }

        private bool InViewport(float component, float limit)
        {
            return component > limit || component < -limit;
        }
    }
}