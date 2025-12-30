using Destroy.Systems;
using UFO.Components;
using UFO.Services;

namespace UFO.Systems
{
    public class UFODestroySystem : DestroyByIdSystem<IUFODataViewService, UFOComponent> { }
}