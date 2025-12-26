using Collisions.Aspects;
using Collisions.Components;
using Components;
using Configs;
using Data;
using EntityTags.Aspects;
using Health.Aspects;
using Health.Components;
using Inputs.Aspects;
using Leopotam.EcsProto;
using Moving.Aspects;
using Moving.Components;
using UI.Services;
using Utils;

namespace Spawn.Systems
{
    public class ShipSpawnSystem : IProtoInitSystem
    {
        public void Init(IProtoSystems systems)
        {
            var world = systems.World();
            
            ShipAspect shipAspect = world.GetAspect<ShipAspect>();
            var shipComponentPool = shipAspect.Pool;
            
            MoveInputEventAspect moveInputEventAspect = world.GetAspect<MoveInputEventAspect>();
            RotationInputEventAspect rotationInputEventAspect = world.GetAspect<RotationInputEventAspect>();

            MovableAspect movableAspect = world.GetAspect<MovableAspect>();
            RotationAspect rotationAspect = world.GetAspect<RotationAspect>();
            MoveSpeedAspect moveSpeedAspect = world.GetAspect<MoveSpeedAspect>();
            MoveSpeedLimitAspect moveSpeedLimitAspect = world.GetAspect<MoveSpeedLimitAspect>();
            AccelerationSpeedAspect accelerationSpeedAspect = world.GetAspect<AccelerationSpeedAspect>();
            SlowdownSpeedAspect slowdownSpeedAspect = world.GetAspect<SlowdownSpeedAspect>();
            CollisionRadiusAspect collisionRadiusAspect = world.GetAspect<CollisionRadiusAspect>();
            ObjectTypeAspect objectTypeAspect = world.GetAspect<ObjectTypeAspect>();
            HealthAspect healthAspect = world.GetAspect<HealthAspect>();
            BulletInputEventAspect bulletInputEventAspect = world.GetAspect<BulletInputEventAspect>();
            LaserInputEventAspect laserInputEventAspect = world.GetAspect<LaserInputEventAspect>();
            TeleportOutsideScreenAspect teleportOutsideScreenAspect = world.GetAspect<TeleportOutsideScreenAspect>();

            shipComponentPool.NewEntity(out ProtoEntity entity);
            moveInputEventAspect.Pool.Add(entity);
            rotationInputEventAspect.Pool.Add(entity);
            bulletInputEventAspect.Pool.Add(entity);
            laserInputEventAspect.Pool.Add(entity);
            teleportOutsideScreenAspect.Pool.Add(entity);
            
            ref MoveSpeedComponent moveSpeedComponent = ref moveSpeedAspect.Pool.Add(entity);
            ref MoveSpeedLimitComponent moveSpeedLimitComponent = ref moveSpeedLimitAspect.Pool.Add(entity);
            ref AccelerationSpeedComponent accelerationSpeedComponent = ref accelerationSpeedAspect.Pool.Add(entity);
            ref SlowdownSpeedComponent slowdownSpeedComponent = ref slowdownSpeedAspect.Pool.Add(entity);
            ref MovableComponent movableComponent = ref movableAspect.Pool.Add(entity);
            ref RotationComponent rotationComponent = ref rotationAspect.Pool.Add(entity);
            ref CollisionRadiusComponent collisionRadiusComponent = ref collisionRadiusAspect.Pool.Add(entity);
            ref ObjectTypeComponent objectTypeComponent = ref objectTypeAspect.Pool.Add(entity);
            ref HealthComponent healthComponent = ref healthAspect.Pool.Add(entity);

            var shipConfig = ShipConfig.LoadFromAssets();
            
            var position = new Point(shipConfig.StartPosition.x, shipConfig.StartPosition.y);
            movableComponent.Position = position;
            rotationComponent.Angle = shipConfig.StartRotation;
            rotationComponent.RotationSpeed = shipConfig.RotationSpeed;
            
            moveSpeedComponent.Value = shipConfig.StartMoveSpeed;
            moveSpeedLimitComponent.Value = shipConfig.MaxMoveSpeed;
            accelerationSpeedComponent.AccelerationSpeed = shipConfig.AccelerationSpeed;
            slowdownSpeedComponent.SlowdownSpeed = shipConfig.SlowdownSpeed;

            collisionRadiusComponent.CollisionRadius = shipConfig.CollisionRadius;
            objectTypeComponent.ObjectType = ObjectType.Ship;
            healthComponent.Value = 1;
            
            var shipDataViewService = systems.GetService<IShipDataViewService>();
            shipDataViewService!.CreateView(shipConfig);
            shipDataViewService.SetPosition(position);
            shipDataViewService.SetShipRotation(shipConfig.StartRotation);
        }
    }
}