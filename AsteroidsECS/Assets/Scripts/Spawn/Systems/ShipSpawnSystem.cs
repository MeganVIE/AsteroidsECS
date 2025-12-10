using Collisions;
using Collisions.Aspects;
using Collisions.Components;
using Components;
using Configs;
using EntityTags.Aspects;
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
            var moveInputEventComponentPool = moveInputEventAspect.Pool;
            RotationInputEventAspect rotationInputEventAspect = world.GetAspect<RotationInputEventAspect>();
            var rotationInputEventComponentPool = rotationInputEventAspect.Pool;

            MovableAspect movableAspect = world.GetAspect<MovableAspect>();
            var movableComponentPool = movableAspect.Pool;
            RotationAspect rotationAspect = world.GetAspect<RotationAspect>();
            var rotationComponentPool = rotationAspect.Pool;
            MoveSpeedAspect moveSpeedAspect = world.GetAspect<MoveSpeedAspect>();
            var moveSpeedComponentPool = moveSpeedAspect.Pool;
            MoveSpeedLimitAspect moveSpeedLimitAspect = world.GetAspect<MoveSpeedLimitAspect>();
            var moveSpeedLimitComponentPool = moveSpeedLimitAspect.Pool;
            AccelerationSpeedAspect accelerationSpeedAspect = world.GetAspect<AccelerationSpeedAspect>();
            var moveSpeedChangeComponentPool = accelerationSpeedAspect.Pool;
            SlowdownSpeedAspect slowdownSpeedAspect = world.GetAspect<SlowdownSpeedAspect>();
            var slowdownSpeedComponentPool = slowdownSpeedAspect.Pool;
            CollisionRadiusAspect collisionRadiusAspect = world.GetAspect<CollisionRadiusAspect>();
            var collisionRadiusComponentPool = collisionRadiusAspect.Pool;
            ObjectTypeAspect objectTypeAspect = world.GetAspect<ObjectTypeAspect>();
            var objectTypeComponentPool = objectTypeAspect.Pool;

            shipComponentPool.NewEntity(out ProtoEntity entity);
            moveInputEventComponentPool.Add(entity);
            rotationInputEventComponentPool.Add(entity);
            
            ref MoveSpeedComponent moveSpeedComponent = ref moveSpeedComponentPool.Add(entity);
            ref MoveSpeedLimitComponent moveSpeedLimitComponent = ref moveSpeedLimitComponentPool.Add(entity);
            ref AccelerationSpeedComponent accelerationSpeedComponent = ref moveSpeedChangeComponentPool.Add(entity);
            ref SlowdownSpeedComponent slowdownSpeedComponent = ref slowdownSpeedComponentPool.Add(entity);
            ref MovableComponent movableComponent = ref movableComponentPool.Add(entity);
            ref RotationComponent rotationComponent = ref rotationComponentPool.Add(entity);
            ref CollisionRadiusComponent collisionRadiusComponent = ref collisionRadiusComponentPool.Add(entity);
            ref ObjectTypeComponent objectTypeComponent = ref objectTypeComponentPool.Add(entity);

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
            
            var shipDataViewService = systems.GetService<IShipDataViewService>();
            shipDataViewService!.CreateView(shipConfig);
            shipDataViewService.SetPosition(position);
            shipDataViewService.SetShipRotation(shipConfig.StartRotation);
        }
    }
}