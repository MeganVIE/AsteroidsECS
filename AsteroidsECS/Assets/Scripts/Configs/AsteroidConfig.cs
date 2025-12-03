using UI;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "AsteroidConfig", menuName = "Asteroid Config", order = 11)]
    public class AsteroidConfig : EntityConfig
    {
        //public float CollisionRadius = .23f;
        public float DelaySpawnTime = 5;

        public static AsteroidConfig LoadFromAssets() => Resources.Load<AsteroidConfig>("AsteroidConfig");
    }
}