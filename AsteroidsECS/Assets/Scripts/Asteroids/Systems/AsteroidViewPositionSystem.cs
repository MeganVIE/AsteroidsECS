using Asteroids.Components;
using Asteroids.Services;
using UI.Services;
using UI.Systems;

namespace Asteroids.Systems
{
    public class AsteroidViewPositionSystem : ObjectIdViewPositionSystem<IAsteroidDataViewService, AsteroidComponent> { }
}