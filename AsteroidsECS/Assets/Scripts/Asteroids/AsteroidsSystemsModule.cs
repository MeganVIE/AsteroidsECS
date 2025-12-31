using System;
using Asteroids.Services;
using Asteroids.Systems;
using Leopotam.EcsProto;
using Moving;

namespace Asteroids
{
    public class AsteroidsSystemsModule : IProtoModule
    {
        public void Init(IProtoSystems systems)
        {
            systems
                .AddSystem(new AsteroidSpawnSystem())
                .AddSystem(new AsteroidPartSpawnSystem())
                .AddSystem(new AsteroidViewPositionSystem())
                .AddSystem(new AsteroidPartViewPositionSystem())
                .AddSystem(new AsteroidsDestroySystem(), 205)
                .AddSystem(new AsteroidPartsDestroySystem(), 210)
                
                .AddService(new AsteroidDataViewService(), typeof(IAsteroidDataViewService))
                .AddService(new AsteroidPartDataViewService(), typeof(IAsteroidPartDataViewService));
        }

        public IProtoAspect[] Aspects()
        {
            return null;
        }

        public Type[] Dependencies()
        {
            return new [] { typeof(MovingSystemsModule) };
        }
    }
}