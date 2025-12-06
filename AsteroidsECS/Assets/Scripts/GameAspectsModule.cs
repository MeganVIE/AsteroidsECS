using System.Collections.Generic;
using CameraData.Aspects;
using EntityTags.Aspects;
using Inputs.Aspects;
using Leopotam.EcsProto;
using Moving;

public class GameAspectsModule : IProtoAspect
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
            
        _aspects.Add(new MovingAspectsModule());
            
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