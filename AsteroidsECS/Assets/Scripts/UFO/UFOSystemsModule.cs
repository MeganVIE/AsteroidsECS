using System;
using Leopotam.EcsProto;
using Moving;
using UFO.Services;
using UFO.Systems;

namespace UFO
{
    public class UFOSystemsModule : IProtoModule
    {
        public void Init(IProtoSystems systems)
        {
            systems
                .AddSystem(new UFOSpawnSystem())
                .AddSystem(new UfoViewPositionSystem())
                .AddSystem(new UFODestroySystem(), 200)
                .AddSystem(new UFOClearSystem(), 300)
                
                .AddService(new UFODataViewService(), typeof(IUFODataViewService));
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