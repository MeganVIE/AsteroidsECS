using UI;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "AsteroidConfig", menuName = "Asteroid Config", order = 11)]
    public class AsteroidConfig : ScriptableObject
    {
        public GameObject ViewPrefab;
        
        public float CollisionRadius = .23f;
        public float StartMoveSpeed = 0;
        public float DelaySpawnTime = 5;

        public static AsteroidConfig LoadFromAssets() => Resources.Load<AsteroidConfig>("AsteroidConfig");
    }
}