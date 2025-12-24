using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "AsteroidPartConfig", menuName = "Asteroid Part Config", order = 11)]
    public class AsteroidPartConfig : ScriptableObject
    {
        public GameObject ViewPrefab;
        
        public float CollisionRadius = .3f;
        public float StartMoveSpeed = 0.004f;
        public int SpawnAmount = 2;

        public static AsteroidPartConfig LoadFromAssets() => Resources.Load<AsteroidPartConfig>("AsteroidPartConfig");
    }
}