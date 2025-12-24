using EntityTags.Components;
using UI.Services;

namespace Destroy.Systems
{
    public class AsteroidPartsDestroySystem : DestroyByIdSystem<IAsteroidPartDataViewService, AsteroidPartComponent> { }
}