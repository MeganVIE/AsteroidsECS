using UnityEngine;

namespace Configs
{
    public abstract class EntityConfig : ScriptableObject
    {
        public GameObject ViewPrefab;
        public float StartMoveSpeed = 0;
    }
}