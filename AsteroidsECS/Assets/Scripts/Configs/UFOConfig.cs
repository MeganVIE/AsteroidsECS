using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "UFOConfig", menuName = "UFO Config", order = 11)]
    public class UFOConfig : EnemyConfig
    {
        public float DelaySpawnTime = 5;

        public static UFOConfig LoadFromAssets() => Resources.Load<UFOConfig>("UFOConfig");
    }
}