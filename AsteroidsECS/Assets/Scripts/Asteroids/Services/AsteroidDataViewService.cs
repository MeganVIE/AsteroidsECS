using System.Collections.Generic;
using Configs;
using Data;
using UnityEngine;

namespace Asteroids.Services
{
    public class AsteroidDataViewService : IAsteroidDataViewService
    {
        private Dictionary<int, GameObject> _asteroids = new();

        public void CreateView(int id, AsteroidConfig config)
        {
            _asteroids[id] = Object.Instantiate(config.ViewPrefab);
        }

        public void SetPosition(int id, Point newPosition)
        {
            _asteroids[id].transform.position = new Vector3(newPosition.X, newPosition.Y);
        }

        public void Destroy(int id)
        {
            Object.Destroy(_asteroids[id]);
            _asteroids.Remove(id);
        }
    }
}