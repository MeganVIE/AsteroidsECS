using System.Collections.Generic;
using Leopotam.EcsProto;
using CameraData.Aspects;
using Collisions.Aspects;
using Destroy.Aspects;
using EntityTags;
using Health.Aspects;
using Inputs;
using Moving;
using Spawn.Aspects;

public class GameAspectsModule : IProtoAspect
{
    private List<IProtoAspect> _aspects;

    private ProtoWorld _world;
        
    public void Init(ProtoWorld world)
    {
        _world = world;
        _aspects = new List<IProtoAspect>();
            
        _aspects.Add(new CameraDataAspect());
        _aspects.Add(new InputsAspectsModule());
            
        _aspects.Add(new EntityTagAspectsModule());
        
        _aspects.Add(new HealthAspect());
        
        _aspects.Add(new SpawnAsteroidPartAspect());
            
        _aspects.Add(new MovingAspectsModule());
        
        _aspects.Add(new CollisionRadiusAspect());
        _aspects.Add(new CollisionTargetAspect());
        _aspects.Add(new ObjectTypeAspect());
        
        _aspects.Add(new DestroyAspect());
        _aspects.Add(new DestroyOutsideScreenAspect());
            
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