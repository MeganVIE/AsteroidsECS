using System.Collections.Generic;
using Inputs.Aspects;
using Leopotam.EcsProto;
using Moving.Aspects;

namespace Aspects
{
    public class GameAspect : IProtoAspect
    {
        private List<IProtoAspect> _aspects;

        private ProtoWorld _world;
        
        public void Init(ProtoWorld world)
        {
            _world = world;
            _aspects = new List<IProtoAspect>();
            
            _aspects.Add(new CameraDataAspect());
            _aspects.Add(new MoveInputEventAspect());
            
            _aspects.Add(new ShipAspect());
            _aspects.Add(new AsteroidAspect());
            
            _aspects.Add(new RotationAspect());
            _aspects.Add(new MoveSpeedAspect());
            _aspects.Add(new MoveSpeedChangeAspect());
            _aspects.Add(new MoveSpeedLimitAspect());
            _aspects.Add(new MovableAspect());
            
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