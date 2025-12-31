using Collisions.Aspects;
using Collisions.Components;
using Data;
using Health.Aspects;
using Leopotam.EcsProto;
using Moving.Aspects;
using Moving.Components;
using Utils;

namespace Collisions.Systems
{
    public class CircleCollisionSystem : IProtoRunSystem, IProtoInitSystem
    {
        MovableAspect _movableAspect;
        CollisionRadiusAspect _collisionRadiusAspect;
        CollisionTargetAspect _collisionTargetAspect;
        CollisionObjectTypeAspect _collisionObjectTypeAspect;
        HealthAspect _healthAspect;
        
        ProtoIt _nonTargetIt;
        ProtoIt _targetIt;

        ProtoWorld _world;

        public void Init(IProtoSystems systems)
        {
            _world = systems.World();
            
            _movableAspect = _world.GetAspect<MovableAspect>();
            _collisionRadiusAspect = _world.GetAspect<CollisionRadiusAspect>();
            _collisionTargetAspect = _world.GetAspect<CollisionTargetAspect>();
            _collisionObjectTypeAspect = _world.GetAspect<CollisionObjectTypeAspect>();
            _healthAspect = _world.GetAspect<HealthAspect>();

            _nonTargetIt = new(new[] { typeof(CollisionRadiusComponent), typeof(CollisionObjectTypeComponent), typeof(MovableComponent) });
            _nonTargetIt.Init(_world);

            _targetIt = new(new[] { typeof(CollisionRadiusComponent), typeof(CollisionTargetComponent), 
                typeof(CollisionObjectTypeComponent), typeof(MovableComponent) });
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
                    CollisionObjectTypeComponent collisionObjectTypeComponent = _collisionObjectTypeAspect.Pool.Get(targetEntity);

                    if (collisionObjectTypeComponent.ObjectType == targetType && HasCollision(entity, targetEntity))
                    {
                        ref var targetHealthComponent = ref _healthAspect.Pool.Get(targetEntity);
                        targetHealthComponent.Value--;

                        ref var entityHealthComponent = ref _healthAspect.Pool.Get(entity);
                        entityHealthComponent.Value--;
                        break;
                    }
                }
            }
        }

        private bool HasCollision(ProtoEntity mainEntity, ProtoEntity targetEntity)
        {
            var mainCollisionRadius = _collisionRadiusAspect.Pool.Get(mainEntity).Value;
            var targetCollisionRadius = _collisionRadiusAspect.Pool.Get(targetEntity).Value;

            var mainPosition = _movableAspect.Pool.Get(mainEntity).Position;
            var targetPosition = _movableAspect.Pool.Get(targetEntity).Position;

            var positionDistance = GetPositionDistance(mainPosition, targetPosition);
            var collisionDistance = GetCollisionDistance(mainCollisionRadius, targetCollisionRadius);

            return positionDistance < collisionDistance;
        }

        private double GetPositionDistance(Point first, Point second)
        {
            var a = second.X - first.X;
            var b = second.Y - first.Y;
            return a * a + b * b;
        }

        private double GetCollisionDistance(float first, float second)
        {
            var a = first + second;
            return a * a;
        }
    }
}