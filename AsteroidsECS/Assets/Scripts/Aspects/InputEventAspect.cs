using Components;
using Leopotam.EcsProto;

namespace Aspects
{
    public class InputEventAspect : IProtoAspect
    {
        public ProtoPool<InputEventComponent> InputEventComponentPool;
        private ProtoWorld _world;
        
        public void Init(ProtoWorld world)
        {
            world.AddAspect (this);
            InputEventComponentPool = new ();
            world.AddPool (InputEventComponentPool);
            _world = world;
        }

        public void PostInit()
        {
            // Дополнительный этап инициализации. Если есть вложенные аспекты,
            // созданные в процессе инициализации этого аспекта - у них должен быть
            // вызван метод PostInit(). Так же вложенные итераторы должны быть
            // инициализированы здесь вызовом метода Init().
        }

        public ProtoWorld World()
        {
            return _world;
        }
    }
}