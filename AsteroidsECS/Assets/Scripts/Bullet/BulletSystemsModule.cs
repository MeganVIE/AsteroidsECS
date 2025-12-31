using System;
using Bullet.Services;
using Bullet.Systems;
using Inputs;
using Leopotam.EcsProto;
using Moving;

namespace Bullet
{
    public class BulletSystemsModule : IProtoModule
    {
        public void Init(IProtoSystems systems)
        {
            systems
                .AddSystem(new BulletSpawnSystem())
                .AddSystem(new BulletViewPositionSystem())
                .AddSystem(new BulletDestroySystem(), 200)
                
                .AddService(new BulletDataViewService(), typeof(IBulletDataViewService));
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