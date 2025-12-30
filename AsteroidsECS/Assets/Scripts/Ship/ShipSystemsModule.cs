using System;
using Inputs;
using Leopotam.EcsProto;
using Moving;
using Ship.Services;
using Ship.Systems;

namespace Ship
{
    public class ShipSystemsModule : IProtoModule
    {
        public void Init(IProtoSystems systems)
        {
            systems
                .AddSystem(new ShipSpawnSystem())
                .AddSystem(new ShipViewSystem())
                .AddSystem(new ShipDestroySystem(), 200)
                
                .AddService(new ShipDataViewService(), typeof(IShipDataViewService));
        }

        public IProtoAspect[] Aspects()
        {
            return null;
        }

        public Type[] Dependencies()
        {
            return new [] { typeof(MovingSystemsModule), typeof(InputsSystemsModule) };
        }
    }
}