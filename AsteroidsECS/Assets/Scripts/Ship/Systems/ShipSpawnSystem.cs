using Collisions.Aspects;
using Collisions.Components;
using Components;
using Configs;
using Data;
using Health.Aspects;
using Health.Components;
using Inputs.Aspects;
using Leopotam.EcsProto;
using Moving.Aspects;
using Moving.Components;
using Ship.Aspects;
using Ship.Services;
using Utils;

namespace Ship.Systems
{
    public class ShipSpawnSystem : IProtoInitSystem
    {
        private ShipConfig _shipConfig;
        private ProtoWorld _world;
        
        public void Init(IProtoSystems systems)
        {
            _shipConfig ??= ShipConfig.LoadFromAssets();
            _world = systems.World();
            
            ShipAspect shipAspect = _world.GetAspect<ShipAspect>();
            shipAspect.Pool.NewEntity(out ProtoEntity entity);

            TeleportOutsideScreenAspect teleportOutsideScreenAspect = _world.GetAspect<TeleportOutsideScreenAspect>();
            teleportOutsideScreenAspect.Pool.Add(entity);

            var position = new Point(_shipConfig.StartPosition.x, _shipConfig.StartPosition.y);
            var rotation = _shipConfig.StartRotation;
            
            SetInputData(entity);
            SetTransformData(entity, position, rotation);
            SetMoveSpeedData(entity);
            SetCollisionData(entity);
            SetHealthData(entity);
            
            var shipDataViewService = systems.GetService<IShipDataViewService>();
            SetViewData(shipDataViewService, position, rotation);
        }

        private void SetInputData(ProtoEntity entity)
        {
            MoveInputEventAspect moveInputEventAspect = _world.GetAspect<MoveInputEventAspect>();
            moveInputEventAspect.Pool.Add(entity);
            RotationInputEventAspect rotationInputEventAspect = _world.GetAspect<RotationInputEventAspect>();
            rotationInputEventAspect.Pool.Add(entity);
            BulletInputEventAspect bulletInputEventAspect = _world.GetAspect<BulletInputEventAspect>();
            bulletInputEventAspect.Pool.Add(entity);
            LaserInputEventAspect laserInputEventAspect = _world.GetAspect<LaserInputEventAspect>();
            laserInputEventAspect.Pool.Add(entity);
        }

        private void SetHealthData(ProtoEntity entity)
        {
            HealthAspect healthAspect = _world.GetAspect<HealthAspect>();
            ref HealthComponent healthComponent = ref healthAspect.Pool.Add(entity);
            healthComponent.Value = 1;
        }

        private void SetTransformData(ProtoEntity entity, Point position, float rotation)
        {
            MovableAspect movableAspect = _world.GetAspect<MovableAspect>();
            ref MovableComponent movableComponent = ref movableAspect.Pool.Add(entity);
            movableComponent.Position = position;
            
            RotationAspect rotationAspect = _world.GetAspect<RotationAspect>();
            ref RotationComponent rotationComponent = ref rotationAspect.Pool.Add(entity);
            rotationComponent.Angle = rotation;
            rotationComponent.RotationSpeed = _shipConfig.RotationSpeed;
        }

        private void SetMoveSpeedData(ProtoEntity entity)
        {
            MoveSpeedAspect moveSpeedAspect = _world.GetAspect<MoveSpeedAspect>();
            ref MoveSpeedComponent moveSpeedComponent = ref moveSpeedAspect.Pool.Add(entity);
            MoveSpeedLimitAspect moveSpeedLimitAspect = _world.GetAspect<MoveSpeedLimitAspect>();
            ref MoveSpeedLimitComponent moveSpeedLimitComponent = ref moveSpeedLimitAspect.Pool.Add(entity);
            AccelerationSpeedAspect accelerationSpeedAspect = _world.GetAspect<AccelerationSpeedAspect>();
            ref AccelerationSpeedComponent accelerationSpeedComponent = ref accelerationSpeedAspect.Pool.Add(entity);
            SlowdownSpeedAspect slowdownSpeedAspect = _world.GetAspect<SlowdownSpeedAspect>();
            ref SlowdownSpeedComponent slowdownSpeedComponent = ref slowdownSpeedAspect.Pool.Add(entity);
            
            moveSpeedComponent.Value = _shipConfig.StartMoveSpeed;
            moveSpeedLimitComponent.Value = _shipConfig.MaxMoveSpeed;
            accelerationSpeedComponent.AccelerationSpeed = _shipConfig.AccelerationSpeed;
            slowdownSpeedComponent.SlowdownSpeed = _shipConfig.SlowdownSpeed;
        }

        private void SetCollisionData(ProtoEntity entity)
        {
            CollisionRadiusAspect collisionRadiusAspect = _world.GetAspect<CollisionRadiusAspect>();
            ref CollisionRadiusComponent collisionRadiusComponent = ref collisionRadiusAspect.Pool.Add(entity);
            CollisionObjectTypeAspect collisionObjectTypeAspect = _world.GetAspect<CollisionObjectTypeAspect>();
            ref CollisionObjectTypeComponent collisionObjectTypeComponent = ref collisionObjectTypeAspect.Pool.Add(entity);
            
            collisionRadiusComponent.Value = _shipConfig.CollisionRadius;
            collisionObjectTypeComponent.ObjectType = ObjectType.Ship;
        }

        private void SetViewData(IShipDataViewService service, Point position, float rotation)
        {
            service.CreateView(_shipConfig);
            service.SetPosition(position);
            service.SetShipRotation(rotation);
        }
    }
}