using System.Collections.Generic;
using Inputs.Aspects;
using Leopotam.EcsProto;

namespace Inputs
{
    public class InputsAspectsModule : IProtoAspect
    {
        private List<IProtoAspect> _aspects;

        private ProtoWorld _world;
        
        public void Init(ProtoWorld world)
        {
            _world = world;
            _aspects = new List<IProtoAspect>();
            
            _aspects.Add(new MoveInputEventAspect());
            _aspects.Add(new RotationInputEventAspect());
            _aspects.Add(new BulletInputEventAspect());
            _aspects.Add(new LaserInputEventAspect());
            
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