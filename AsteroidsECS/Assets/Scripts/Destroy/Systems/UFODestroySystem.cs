using EntityTags.Components;
using UI.Services;

namespace Destroy.Systems
{
    public class UFODestroySystem : DestroyByIdSystem<IUFODataViewService, UFOComponent> { }
}