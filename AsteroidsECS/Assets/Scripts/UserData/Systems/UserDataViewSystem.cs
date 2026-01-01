using Components;
using Health.Aspects;
using Health.Components;
using Laser.Aspects;
using Laser.Components;
using Leopotam.EcsProto;
using Moving.Aspects;
using Moving.Components;
using Ship.Components;
using UserData.Services;
using Utils;

namespace UserData.Systems
{
    public class UserDataViewSystem : IProtoInitSystem, IProtoRunSystem
    {
        private HealthAspect _healthAspect;
        private MovableAspect _movableAspect;
        private RotationAspect _rotationAspect;
        private MoveSpeedAspect _moveSpeedAspect;

        private LaserAmountAspect _laserAmountAspect;
        private LaserAmountRechargeAspect _laserAmountRechargeAspect;
        
        private IUserDataViewService _userDataViewService;
        
        private ProtoIt _shipIt;
        private ProtoIt _laserIt;
        
        public void Init(IProtoSystems systems)
        {
            var world = systems.World();

            _healthAspect = world.GetAspect<HealthAspect>();
            _movableAspect = world.GetAspect<MovableAspect>();
            _rotationAspect = world.GetAspect<RotationAspect>();
            _moveSpeedAspect = world.GetAspect<MoveSpeedAspect>();

            _laserAmountAspect = world.GetAspect<LaserAmountAspect>();
            _laserAmountRechargeAspect = world.GetAspect<LaserAmountRechargeAspect>();
            
            _userDataViewService = systems.GetService<IUserDataViewService>();

            _shipIt = new(new[]
            {
                typeof(ShipComponent), typeof(HealthComponent), typeof(MovableComponent),
                typeof(RotationComponent), typeof(MoveSpeedComponent)
            });
            _shipIt.Init(world);

            
            _laserIt = new(new[] { typeof(LaserAmountComponent), typeof(LaserAmountRechargeComponent) });
            _laserIt.Init(world);
        }

        public void Run()
        {
           foreach (var entity in _shipIt)
           {
               HealthComponent healthComponent = _healthAspect.Pool.Get(entity);
               _userDataViewService.SetHealth(healthComponent.Value);
               MovableComponent movableComponent = _movableAspect.Pool.Get(entity);
               _userDataViewService.SetPosition(movableComponent.Position);
               RotationComponent rotationComponent = _rotationAspect.Pool.Get(entity);
               _userDataViewService.SetRotation(rotationComponent.Angle);
               MoveSpeedComponent moveSpeedComponent = _moveSpeedAspect.Pool.Get(entity);
               _userDataViewService.SetSpeed(moveSpeedComponent.Value);
           }
            
           foreach (var entity in _laserIt)
           {
               LaserAmountComponent laserAmountComponent = _laserAmountAspect.Pool.Get(entity);
               _userDataViewService.SetLaserAmount(laserAmountComponent.Value);
               LaserAmountRechargeComponent laserAmountRechargeComponent = _laserAmountRechargeAspect.Pool.Get(entity);
               _userDataViewService.SetLaserRechargeTime(laserAmountRechargeComponent.Value);
           }
        }
    }
}