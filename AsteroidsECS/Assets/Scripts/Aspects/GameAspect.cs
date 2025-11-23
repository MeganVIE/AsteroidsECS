using Leopotam.EcsProto;

namespace Aspects
{
    public class GameAspect : IProtoAspect
    {
        private ShipAspect _shipAspect;
        private MovableAspect _movableAspect;
        private InputEventAspect _inputEventAspect;
        private AccelerationAspect _accelerationAspect;
        private ProtoWorld _world;
        
        public void Init(ProtoWorld world)
        {
            _world = world;
            _shipAspect = new ShipAspect();
            _movableAspect = new MovableAspect();
            _inputEventAspect = new InputEventAspect();
            _accelerationAspect = new AccelerationAspect();
            
            _shipAspect.Init(_world);
            _movableAspect.Init(_world);
            _inputEventAspect.Init(_world);
            _accelerationAspect.Init(_world);
        }

        public void PostInit()
        {
            _shipAspect.PostInit();
            _movableAspect.PostInit();
            _inputEventAspect.PostInit();
            _accelerationAspect.PostInit();
        }

        public ProtoWorld World()
        {
            return _world;
        }
    }
}