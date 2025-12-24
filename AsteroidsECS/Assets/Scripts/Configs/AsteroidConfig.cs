using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "AsteroidConfig", menuName = "Asteroid Config", order = 11)]
    public class AsteroidConfig : ScriptableObject
    {
        public GameObject ViewPrefab;
        
        public float CollisionRadius = .5f;
        public float StartMoveSpeed = 0.002f;
        public float DelaySpawnTime = 2;

        public static AsteroidConfig LoadFromAssets() => Resources.Load<AsteroidConfig>("AsteroidConfig");
    }
}