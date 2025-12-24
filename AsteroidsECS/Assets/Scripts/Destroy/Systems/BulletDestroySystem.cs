using EntityTags.Components;
using UI.Services;

namespace Destroy.Systems
{
    public class BulletDestroySystem : DestroyByIdSystem<IBulletDataViewService, BulletComponent> { }
}