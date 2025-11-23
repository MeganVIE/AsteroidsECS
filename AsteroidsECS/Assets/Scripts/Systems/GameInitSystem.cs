using Aspects;
using Components;
using Configs;
using Leopotam.EcsProto;
using UnityEngine;

namespace Systems
{
    public class GameInitSystem : IProtoInitSystem
    {
        ProtoWorld _world;
        
        private IShipDataViewProvider _shipDataViewProvider;

        public GameInitSystem(IShipDataViewProvider shipDataViewProvider)
        {
            _shipDataViewProvider = shipDataViewProvider;
        }

        public void Init(IProtoSystems systems)
        {
            _world = systems.World();
            
            // Правильный способ доступа к пулу.
            ShipAspect shipAspect = (ShipAspect)_world.Aspect(typeof(ShipAspect));
            var shipComponentPool = shipAspect.ShipComponentPool;
            MovableAspect movableAspect = (MovableAspect)_world.Aspect(typeof(MovableAspect));
            var movableComponentPool = movableAspect.MovableComponentPool;
            InputEventAspect inputEventAspect = (InputEventAspect)_world.Aspect(typeof(InputEventAspect));
            var inputEventComponentPool = inputEventAspect.InputEventComponentPool;
            AccelerationAspect accelerationAspect = (AccelerationAspect)_world.Aspect(typeof(AccelerationAspect));
            var accelerationComponentPool = accelerationAspect.AccelerationComponentPool;

            // NewEntity() создает сущность и добавляет на нее компонент из пула.
            ref ShipComponent shipComponent = ref shipComponentPool.NewEntity(out ProtoEntity entity);

            // Add() добавляет компонент к сущности.
            // Если компонент уже существует - будет брошено исключение в DEBUG-версии.
            ref MovableComponent movableComponent = ref movableComponentPool.Add(entity);
            ref InputEventComponent inputEventComponent = ref inputEventComponentPool.Add(entity);
            ref AccelerationComponent accelerationComponent = ref accelerationComponentPool.Add(entity);

            var shipConfig = ShipConfig.LoadFromAssets();
            var shipGO = Object.Instantiate(shipConfig.ViewPrefab,shipConfig.StartPosition, Quaternion.Euler(0, 0, shipConfig.StartRotation));
            _shipDataViewProvider.SetShipObject(shipGO);
            //_shipDataViewProvider.SetShipPosition(shipConfig.StartPosition);
            movableComponent.Position = shipConfig.StartPosition;
            accelerationComponent.AccelerationSpeed = shipConfig.AccelerationSpeed;
            accelerationComponent.SlowdownSpeed = shipConfig.SlowdownSpeed;
            accelerationComponent.Acceleration = shipConfig.StartAcceleration;
            accelerationComponent.MaxAcceleration = shipConfig.MaxAcceleration;
        }
    }
}