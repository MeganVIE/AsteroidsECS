using Bullet.Components;
using Bullet.Services;
using Destroy.Systems;

namespace Bullet.Systems
{
    public class BulletDestroySystem : DestroyByIdSystem<IBulletDataViewService, BulletComponent> { }
}