using System;
using Destroy;
using Leopotam.EcsProto;
using Score.Systems;

namespace Score
{
    public class ScoreSystemsModule : IProtoModule
    {
        public void Init(IProtoSystems systems)
        {
            systems
                .AddSystem(new ScoreChangeAtDeathSystem(), 50)
                .AddSystem(new ScoreChangeSystem(), 300);
        }

        public IProtoAspect[] Aspects()
        {
            return null;
        }

        public Type[] Dependencies()
        {
            return new [] { typeof(DestroySystemsModule) };
        }
    }
}