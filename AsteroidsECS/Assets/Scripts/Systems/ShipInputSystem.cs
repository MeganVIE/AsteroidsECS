using Aspects;
using Components;
using Inputs;
using Leopotam.EcsProto;

namespace Systems
{
    public class ShipInputSystem : IProtoInitSystem, IProtoRunSystem
    {
        InputEventAspect _aspect;
        ProtoIt _it;

        private IInputSystem _inputSystem;

        public ShipInputSystem(IInputSystem inputSystem)
        {
            _inputSystem = inputSystem;
        }

        public void Init(IProtoSystems systems)
        {
            // Получаем экземпляр мира по умолчанию.
            ProtoWorld world = systems.World();
            // Получаем аспект мира (из примера выше) и кешируем его.
            _aspect = (InputEventAspect)world.Aspect(typeof(InputEventAspect));
            // Создаем итератор с явным указанием типов требуемых (include) компонентов.
            _it = new(new[] { typeof(InputEventComponent) });
            // Инициализируем его для указания, из какого мира берутся данные.
            _it.Init(world);
        }
        
        public void Run()
        { 
            // Мы хотим получить все сущности с компонентом "Component1".
            foreach (ProtoEntity entity in _it) 
            {
                // получаем доступ к компоненту на отфильтрованной сущности.
                ref InputEventComponent component = ref _aspect.InputEventComponentPool.Get(entity);

                component.IsMovePressing = _inputSystem.MovePressing;
            }
        }
    }
}