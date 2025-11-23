using System;
using Aspects;
using Components;
using Leopotam.EcsProto;

namespace Systems
{
    public class AccelerationSystem : IProtoInitSystem, IProtoRunSystem
    {
        AccelerationAspect _accelerationAspect;
        InputEventAspect _inputEventAspect;
        ProtoIt _it;

        private IDeltaTimeSource _deltaTimeSource;

        public AccelerationSystem(IDeltaTimeSource deltaTimeSource)
        {
            _deltaTimeSource = deltaTimeSource;
        }

        public void Init(IProtoSystems systems)
        {
            // Получаем экземпляр мира по умолчанию.
            ProtoWorld world = systems.World();
            // Получаем аспект мира (из примера выше) и кешируем его.
            _accelerationAspect = (AccelerationAspect)world.Aspect(typeof(AccelerationAspect));
            _inputEventAspect = (InputEventAspect)world.Aspect(typeof(InputEventAspect));
            // Создаем итератор с явным указанием типов требуемых (include) компонентов.
            _it = new(new[] { typeof(AccelerationComponent), typeof(InputEventComponent) });
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
                InputEventComponent inputEventComponent = _inputEventAspect.InputEventComponentPool.Get(entity);

                if (inputEventComponent.IsMovePressing)
                {
                    accelerationComponent.Acceleration += accelerationComponent.AccelerationSpeed * _deltaTimeSource.DeltaTime;
                    accelerationComponent.Acceleration = Math.Min(accelerationComponent.Acceleration, accelerationComponent.MaxAcceleration);
                }
                else
                {
                    accelerationComponent.Acceleration -= accelerationComponent.Acceleration * (_deltaTimeSource.DeltaTime / accelerationComponent.SlowdownSpeed);
                }
            }
        }
    }
}