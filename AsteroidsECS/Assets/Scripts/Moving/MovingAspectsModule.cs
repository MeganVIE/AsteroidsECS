using System.Collections.Generic;
using Leopotam.EcsProto;
using Moving.Aspects;

namespace Moving
{
    public class MovingAspectsModule : IProtoAspect
    {
        private List<IProtoAspect> _aspects;

        private ProtoWorld _world;
        
        public void Init(ProtoWorld world)
        {
            _world = world;
            _aspects = new List<IProtoAspect>();
            
            _aspects.Add(new RotationAspect());
            _aspects.Add(new MoveSpeedAspect());
            _aspects.Add(new AccelerationSpeedAspect());
            _aspects.Add(new SlowdownSpeedAspect());
            _aspects.Add(new MoveSpeedLimitAspect());
            _aspects.Add(new MovableAspect());
            _aspects.Add(new DestroyOutsideScreenAspect());
            _aspects.Add(new TeleportOutsideScreenAspect());
            
            _aspects.ForEach(a => a.Init(_world));
        }

        public void PostInit()
        {
            _aspects.ForEach(a => a.PostInit());
        }

        public ProtoWorld World()
        {
            return _world;
        }
    }
}