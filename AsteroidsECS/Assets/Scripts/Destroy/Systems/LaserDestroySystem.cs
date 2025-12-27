using EntityTags.Components;
using UI.Services;

namespace Destroy.Systems
{
    public class LaserDestroySystem : DestroyByIdSystem<ILaserDataViewService, LaserComponent> { }
}