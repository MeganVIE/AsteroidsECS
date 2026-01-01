using System;
using Destroy.Systems;
using Leopotam.EcsProto;

namespace Destroy
{
    public class DestroySystemsModule : IProtoModule
    {
        public void Init(IProtoSystems systems)
        {
            systems
                .AddSystem(new DestroyOutsideScreenSystem())
                .AddSystem(new DestroyByTimerSystem());
        }

        public IProtoAspect[] Aspects()
        {
            return null;
        }

        public Type[] Dependencies()
        {
            return null;
        }
    }
}