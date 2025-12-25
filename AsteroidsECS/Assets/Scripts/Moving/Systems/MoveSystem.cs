using System;
using Components;
using Data;
using Leopotam.EcsProto;
using Moving.Aspects;
using Moving.Components;
using UnityEngine;
using Utils;

namespace Moving.Systems
{
    public class MoveSystem : IProtoRunSystem, IProtoInitSystem
    {
        MoveSpeedAspect _moveSpeedAspect;
        MovableAspect _movableAspect;
        RotationAspect _rotationAspect;
        ProtoIt _it;

        public void Init(IProtoSystems systems)
        {
            ProtoWorld world = systems.World();
            
            _moveSpeedAspect = world.GetAspect<MoveSpeedAspect>();
            _movableAspect = world.GetAspect<MovableAspect>();
            _rotationAspect = world.GetAspect<RotationAspect>();
            
            _it = new(new[] { typeof(MoveSpeedComponent), typeof(MovableComponent), typeof(RotationComponent) });
            _it.Init(world);
        }
        
        public void Run()
        {
            foreach (ProtoEntity entity in _it) 
            {
                MoveSpeedComponent moveSpeedComponent = _moveSpeedAspect.Pool.Get(entity);
                ref MovableComponent movableComponent = ref _movableAspect.Pool.Get(entity);
                RotationComponent rotationComponent = _rotationAspect.Pool.Get(entity);

                double angleRad = Math.PI * rotationComponent.Angle / 180.0;
                
                Vector2 point = Vector2.up * moveSpeedComponent.Value;
                double newX = point.x * Math.Cos(angleRad) - point.y * Math.Sin(angleRad);
                double newY = point.x * Math.Sin(angleRad) + point.y * Math.Cos(angleRad);
                
                movableComponent.Position += new Point((float)newX, (float)newY);
            }
        }
    }
}