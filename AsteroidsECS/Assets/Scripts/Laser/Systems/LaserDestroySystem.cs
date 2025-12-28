using Destroy.Systems;
using Laser.Components;
using Laser.Services;

namespace Laser.Systems
{
    public class LaserDestroySystem : DestroyByIdSystem<ILaserDataViewService, LaserComponent> { }
}