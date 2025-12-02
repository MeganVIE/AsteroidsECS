using Aspects;
using Components;
using Configs;
using Inputs.Aspects;
using Leopotam.EcsProto;
using Moving.Aspects;
using Moving.Components;
using UI;

namespace Systems
{
    public class ShipCreateSystem : IProtoInitSystem
    {
        ProtoWorld _world;

        public void Init(IProtoSystems systems)
        {
            _world = systems.World();
            
            ShipAspect shipAspect = _world.GetAspect<ShipAspect>();
            var shipComponentPool = shipAspect.Pool;
            MovableAspect movableAspect = _world.GetAspect<MovableAspect>();
            var movableComponentPool = movableAspect.Pool;
            RotationAspect rotationAspect = _world.GetAspect<RotationAspect>();
            var rotationComponentPool = rotationAspect.Pool;
            MoveInputEventAspect moveInputEventAspect = _world.GetAspect<MoveInputEventAspect>();
            var inputEventComponentPool = moveInputEventAspect.Pool;
            MoveSpeedAspect moveSpeedAspect = _world.GetAspect<MoveSpeedAspect>();
            var moveSpeedComponentPool = moveSpeedAspect.Pool;
            MoveSpeedLimitAspect moveSpeedLimitAspect = _world.GetAspect<MoveSpeedLimitAspect>();
            var moveSpeedLimitComponentPool = moveSpeedLimitAspect.Pool;
            MoveSpeedChangeAspect moveSpeedChangeAspect = _world.GetAspect<MoveSpeedChangeAspect>();
            var moveSpeedChangeComponentPool = moveSpeedChangeAspect.Pool;

            shipComponentPool.NewEntity(out ProtoEntity entity);
            inputEventComponentPool.Add(entity);
            
            ref MoveSpeedComponent moveSpeedComponent = ref moveSpeedComponentPool.Add(entity);
            ref MoveSpeedLimitComponent moveSpeedLimitComponent = ref moveSpeedLimitComponentPool.Add(entity);
            ref MoveSpeedChangeComponent moveSpeedChangeComponent = ref moveSpeedChangeComponentPool.Add(entity);
            ref MovableComponent movableComponent = ref movableComponentPool.Add(entity);
            ref RotationComponent rotationComponent = ref rotationComponentPool.Add(entity);

            var shipConfig = ShipConfig.LoadFromAssets();
            
            var shipDataViewService = systems.GetService<IShipDataViewService>();
            shipDataViewService!.CreateShipView(shipConfig);
            
            rotationComponent.Angle = shipConfig.StartRotation;
            rotationComponent.RotationSpeed = shipConfig.RotationSpeed;
            movableComponent.Position = new Point(shipConfig.StartPosition.x, shipConfig.StartPosition.y);
            
            moveSpeedComponent.Value = shipConfig.StartMoveSpeed;
            moveSpeedLimitComponent.Value = shipConfig.MaxMoveSpeed;
            moveSpeedChangeComponent.AccelerationSpeed = shipConfig.AccelerationSpeed;
            moveSpeedChangeComponent.SlowdownSpeed = shipConfig.SlowdownSpeed;
        }
    }
}