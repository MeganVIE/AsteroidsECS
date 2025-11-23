using Aspects;
using Components;
using Leopotam.EcsProto;
using UnityEngine;

namespace Systems
{
    public class AccelerationMoveSystem : IProtoRunSystem, IProtoInitSystem
    {
        AccelerationAspect _accelerationAspect;
        MovableAspect _movableAspect;
        ProtoIt _it;

        //private IDeltaTimeSource _deltaTimeSource;

        /*public AccelerationMoveSystem(IDeltaTimeSource deltaTimeSource)
        {
            _deltaTimeSource = deltaTimeSource;
        }*/

        public void Init(IProtoSystems systems)
        {
            // Получаем экземпляр мира по умолчанию.
            ProtoWorld world = systems.World();
            // Получаем аспект мира (из примера выше) и кешируем его.
            _accelerationAspect = (AccelerationAspect)world.Aspect(typeof(AccelerationAspect));
            _movableAspect = (MovableAspect)world.Aspect(typeof(MovableAspect));
            // Создаем итератор с явным указанием типов требуемых (include) компонентов.
            _it = new(new[] { typeof(AccelerationComponent), typeof(MovableComponent) });
            // Инициализируем его для указания, из какого мира берутся данные.
            _it.Init(world);
        }
        
        public void Run()
        {
            // Мы хотим получить все сущности с компонентом "Component1".
            foreach (ProtoEntity entity in _it) 
            {
                // получаем доступ к компоненту на отфильтрованной сущности.
                ref AccelerationComponent accelerationComponent = ref _accelerationAspect.AccelerationComponentPool.Get(entity);
                ref MovableComponent movableComponent = ref _movableAspect.MovableComponentPool.Get(entity);

                movableComponent.Position = movableComponent.Position + Vector2.up * accelerationComponent.Acceleration;
            }
        }
    }
}