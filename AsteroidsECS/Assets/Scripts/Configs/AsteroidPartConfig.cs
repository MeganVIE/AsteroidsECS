using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "AsteroidPartConfig", menuName = "Asteroid Part Config", order = 11)]
    public class AsteroidPartConfig : EnemyConfig
    {
        public int SpawnAmount = 2;

        public static AsteroidPartConfig LoadFromAssets() => Resources.Load<AsteroidPartConfig>("AsteroidPartConfig");
    }
}