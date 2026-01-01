using System.Collections.Generic;
using Asteroids;
using Bullet.Aspects;
using Leopotam.EcsProto;
using CameraData.Aspects;
using Collisions;
using Destroy;
using EntityTags.Aspects;
using Health.Aspects;
using Inputs;
using Laser;
using Moving;
using Score;
using Ship.Aspects;
using UFO.Aspects;

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
        
        _aspects.Add(new LaserAspectsModule());
        _aspects.Add(new AsteroidsAspectsModule());

        _aspects.Add(new MovingAspectsModule());
        _aspects.Add(new CollisionAspectsModule());
        
        _aspects.Add(new ScoreAspectsModule());
        _aspects.Add(new DestroyAspectsModule());
        
        _aspects.Add(new ObjectIdAspect());
        _aspects.Add(new ShipAspect());
        _aspects.Add(new BulletAspect());
        _aspects.Add(new UFOAspect());
        
        _aspects.Add(new HealthAspect());
            
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