using System.Collections.Generic;
using Configs;
using Data;
using UnityEngine;

namespace Laser.Services
{
    public class LaserDataViewService : ILaserDataViewService
    {
        private Dictionary<int, GameObject> _lasers = new();

        public void CreateView(int id, LaserConfig config)
        {
            _lasers[id] = Object.Instantiate(config.ViewPrefab);
        }

        public void SetRotation(int id, float angle)
        {
            _lasers[id].transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        public void SetLength(int id, float length)
        {
            LineRenderer lineRenderer = _lasers[id].GetComponentInChildren<LineRenderer>();

            if (lineRenderer != null)
            {
                lineRenderer.SetPosition(1, new Vector3(0, length));
            }
        }

        public void SetPosition(int id, Point newPosition)
        {
            _lasers[id].transform.position = new Vector3(newPosition.X, newPosition.Y);
        }

        public void Destroy(int id)
        {
            Object.Destroy(_lasers[id]);
            _lasers.Remove(id);
        }
    }
}