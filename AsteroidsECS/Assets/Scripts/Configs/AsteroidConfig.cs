using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "AsteroidConfig", menuName = "Asteroid Config", order = 11)]
    public class AsteroidConfig : EnemyConfig
    {
        public float DelaySpawnTime = 2;

        public static AsteroidConfig LoadFromAssets() => Resources.Load<AsteroidConfig>("AsteroidConfig");
    }
}