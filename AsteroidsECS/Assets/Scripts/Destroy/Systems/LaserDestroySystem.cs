using EntityTags.Components;
using Laser.Components;
using Laser.Services;
using UI.Services;

namespace Destroy.Systems
{
    public class LaserDestroySystem : DestroyByIdSystem<ILaserDataViewService, LaserComponent> { }
}