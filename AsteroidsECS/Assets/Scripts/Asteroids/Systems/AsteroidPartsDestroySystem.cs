using Asteroids.Components;
using Asteroids.Services;
using Destroy.Systems;
using UI.Services;

namespace Asteroids.Systems
{
    public class AsteroidPartsDestroySystem : DestroyByIdSystem<IAsteroidPartDataViewService, AsteroidPartComponent> { }
}