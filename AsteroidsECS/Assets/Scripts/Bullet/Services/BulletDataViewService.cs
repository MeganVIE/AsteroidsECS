using System.Collections.Generic;
using Configs;
using Data;
using UI.Services;
using UnityEngine;

namespace Bullet.Services
{
    public class BulletDataViewService : IBulletDataViewService
    {
        private Dictionary<int, GameObject> _bullets = new();

        public void CreateView(int id, BulletConfig config)
        {
            _bullets[id] = Object.Instantiate(config.ViewPrefab);
        }

        public void SetPosition(int id, Point newPosition)
        {
            _bullets[id].transform.position = new Vector3(newPosition.X, newPosition.Y);
        }

        public void Destroy(int id)
        {
            Object.Destroy(_bullets[id]);
            _bullets.Remove(id);
        }
    }
}