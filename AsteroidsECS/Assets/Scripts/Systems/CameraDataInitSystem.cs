using Aspects;
using Components;
using Leopotam.EcsProto;
using Utils;

namespace Systems
{
    public class CameraDataInitSystem : IProtoInitSystem
    {
        public void Init(IProtoSystems systems)
        {
            var world = systems.World();

            CameraDataAspect cameraDataAspect = world.GetAspect<CameraDataAspect>();
            var cameraDataComponentPool = cameraDataAspect.Pool;

            ref CameraDataComponent cameraDataComponent = ref cameraDataComponentPool.NewEntity(out ProtoEntity entity);

            var cameraDataService = systems.GetService<ICameraDataService>();
            var viewportData = cameraDataService.GetHalfViewport();

            cameraDataComponent.HalfViewportWidth = viewportData.X;
            cameraDataComponent.HalfViewportHeight = viewportData.Y;
        }
    }
}