using Asteroids.Components;
using Asteroids.Services;
using Destroy.Systems;

namespace Asteroids.Systems
{
    public class AsteroidPartsDestroySystem : DestroyByIdSystem<IAsteroidPartDataViewService, AsteroidPartComponent> { }
}