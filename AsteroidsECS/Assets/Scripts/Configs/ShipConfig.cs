using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "NewShipConfig", menuName = "Ship Config", order = 10)]
    public class ShipConfig : ScriptableObject
    {
        public GameObject ViewPrefab;
        //public float CollisionRadius = .3f;
        
        public float AccelerationSpeed = 0.005f;
        public float MaxMoveSpeed = 0.015f;
        public float SlowdownSpeed = 1f;
        public float RotationSpeed = 90;
        
        public Vector2 StartPosition = Vector2.zero;
        public float StartRotation = 0;
        public float StartMoveSpeed = 0;

        public static ShipConfig LoadFromAssets() => Resources.Load<ShipConfig>("ShipConfig");
    }
}