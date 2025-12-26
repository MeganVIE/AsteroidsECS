using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "LaserConfig", menuName = "Laser Config", order = 12)]
    public class LaserConfig : ScriptableObject
    {
        public GameObject ViewPrefab;
        
        public float CollisionLength = 10f;
        public float LifeTime = 1f;
        public byte MaxAmount = 5;
        public float AmountRechargeTime = 2f;
        
        public static LaserConfig LoadFromAssets() => Resources.Load<LaserConfig>("LaserConfig");
    }
}