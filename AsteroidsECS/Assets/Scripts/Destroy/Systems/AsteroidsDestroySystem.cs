using EntityTags.Components;
using UI.Services;

namespace Destroy.Systems
{
    public class AsteroidsDestroySystem : DestroyByIdSystem<IAsteroidDataViewService, AsteroidComponent> { }
}