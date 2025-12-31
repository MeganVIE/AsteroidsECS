using System;
using Collisions.Aspects;
using Collisions.Components;
using Data;
using Health.Aspects;
using Leopotam.EcsProto;
using Moving.Aspects;
using Moving.Components;
using Utils;

namespace Laser.Systems
{
    public class LaserCollisionSystem : IProtoRunSystem, IProtoInitSystem
    {
        private MovableAspect _movableAspect;
        private RotationAspect _rotationAspect;
        
        private CollisionLengthAspect _collisionLengthAspect;
        private CollisionRadiusAspect _collisionRadiusAspect;
        private CollisionTargetAspect _collisionTargetAspect;
        private CollisionObjectTypeAspect _collisionObjectTypeAspect;
        private HealthAspect _healthAspect;
        
        private ProtoIt _nonTargetIt;
        private ProtoIt _targetIt;

        private ProtoWorld _world;

        public void Init(IProtoSystems systems)
        {
            _world = systems.World();
            
            _movableAspect = _world.GetAspect<MovableAspect>();
            _rotationAspect = _world.GetAspect<RotationAspect>();
            _collisionLengthAspect = _world.GetAspect<CollisionLengthAspect>();
            _collisionRadiusAspect = _world.GetAspect<CollisionRadiusAspect>();
            _collisionTargetAspect = _world.GetAspect<CollisionTargetAspect>();
            _collisionObjectTypeAspect = _world.GetAspect<CollisionObjectTypeAspect>();
            _healthAspect = _world.GetAspect<HealthAspect>();

            _nonTargetIt = new(new[] { typeof(CollisionRadiusComponent), typeof(CollisionObjectTypeComponent), typeof(MovableComponent) });
            _nonTargetIt.Init(_world);

            _targetIt = new(new[] { typeof(CollisionLengthComponent), typeof(CollisionTargetComponent), 
                typeof(CollisionObjectTypeComponent), typeof(MovableComponent) });
            _targetIt.Init(_world);
        }

        public void Run()
        {
            foreach (var entity in _targetIt)
            {
                ObjectType targetType = _collisionTargetAspect.Pool.Get(entity).Target;

                foreach (var targetEntity in _nonTargetIt)
                {
                    CollisionObjectTypeComponent collisionObjectTypeComponent = _collisionObjectTypeAspect.Pool.Get(targetEntity);

                    if (collisionObjectTypeComponent.ObjectType == targetType)
                    {
                        var mainCollisionLength = _collisionLengthAspect.Pool.Get(entity).Value;
                        var targetCollisionRadius = _collisionRadiusAspect.Pool.Get(targetEntity).Value;

                        var mainRotation = _rotationAspect.Pool.Get(entity).Angle;
                        var mainPosition = _movableAspect.Pool.Get(entity).Position;
                        var targetPosition = _movableAspect.Pool.Get(targetEntity).Position;

                        double angleRad = Math.PI * mainRotation / 180.0;
                        Point point = new Point(0, 1);
                        double newX = point.X * Math.Cos(angleRad) - point.Y * Math.Sin(angleRad);
                        double newY = point.X * Math.Sin(angleRad) + point.Y * Math.Cos(angleRad);

                        Point direction = new Point((float)newX, (float)newY);
                        var vectorMT = targetPosition - mainPosition;

                        var projectPoint = mainPosition + direction *
                            (vectorMT.X * direction.X + vectorMT.Y * direction.Y) /
                            (direction.X * direction.X + direction.Y * direction.Y);

                        var perpendicularSqrLength = (projectPoint.X - targetPosition.X) * (projectPoint.X - targetPosition.X)
                                                     + (projectPoint.Y - targetPosition.Y) * (projectPoint.Y - targetPosition.Y);
                        var projectVector = projectPoint - mainPosition;
                        var projectVectorSqrLength = projectVector.X * projectVector.X + projectVector.Y * projectVector.Y;

                        bool hasCollision = perpendicularSqrLength < targetCollisionRadius * targetCollisionRadius
                                            && projectVector.X * direction.X + projectVector.Y * direction.Y > 0
                                            && projectVectorSqrLength < mainCollisionLength * mainCollisionLength;

                        if (hasCollision)
                        {
                            ref var targetHealthComponent = ref _healthAspect.Pool.Get(targetEntity);
                            targetHealthComponent.Value--;
                            break;
                        }
                    }
                }
            }
        }
    }
}