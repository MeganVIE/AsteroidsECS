using UnityEngine;

namespace Utils
{
    public class RandomService : IRandomService
    {
        public int GetRandom(int a, int b)
        {
            return Random.Range(a, b);
        }

        public float GetRandom(float a, float b)
        {
            return Random.Range(a, b);
        }
    }
}