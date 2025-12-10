using System;
using Collisions.Aspects;
using Collisions.Components;
using Leopotam.EcsProto;
using Moving.Aspects;
using Moving.Components;
using Utils;

namespace Collisions.Systems
{
    public class MovableCollisionSystem : IProtoRunSystem, IProtoInitSystem
    {
        MovableAspect _movableAspect;
        CollisionRadiusAspect _collisionRadiusAspect;
        CollisionTargetAspect _collisionTargetAspect;
        ObjectTypeAspect _objectTypeAspect;
        ProtoItExc _nonTargetIt;
        ProtoIt _targetIt;

        ProtoWorld _world;

        public void Init(IProtoSystems systems)
        {
            _world = systems.World();
            
            _movableAspect = _world.GetAspect<MovableAspect>();
            _collisionRadiusAspect = _world.GetAspect<CollisionRadiusAspect>();
            _collisionTargetAspect = _world.GetAspect<CollisionTargetAspect>();
            _objectTypeAspect = _world.GetAspect<ObjectTypeAspect>();

            _nonTargetIt = new(new[] { typeof(CollisionRadiusComponent), typeof(ObjectTypeComponent), typeof(MovableComponent) },
                new[] { typeof(CollisionTargetComponent) });
            _nonTargetIt.Init(_world);

            _targetIt = new(new[] { typeof(CollisionRadiusComponent), typeof(CollisionTargetComponent), typeof(ObjectTypeComponent), typeof(MovableComponent) });
            _targetIt.Init(_world);
        }
        
        public void Run()
        {
            foreach (var entity in _targetIt)
            {
                CollisionTargetComponent collisionTargetComponent = _collisionTargetAspect.Pool.Get(entity);
                ObjectType targetType = collisionTargetComponent.Target;

                foreach (var targetEntity in _nonTargetIt)
                {
                    ObjectTypeComponent objectTypeComponent = _objectTypeAspect.Pool.Get(targetEntity);

                    if (objectTypeComponent.ObjectType == targetType)
                    {
                        var collisionRadius = _collisionRadiusAspect.Pool.Get(entity).CollisionRadius;
                        var targetCollisionRadius = _collisionRadiusAspect.Pool.Get(targetEntity).CollisionRadius;

                        var mainPosition = _movableAspect.Pool.Get(entity).Position;
                        var targetPosition = _movableAspect.Pool.Get(targetEntity).Position;

                        var a = targetPosition.X - mainPosition.X;
                        var b = targetPosition.Y - mainPosition.Y;
                        var positionDistance = a * a + b * b;
                        var collisionDistance = Math.Pow(collisionRadius + targetCollisionRadius, 2f);
                        
                        if (positionDistance < collisionDistance)
                        {
                            // damage
                        }
                    }
                }
            }
        }
    }
}