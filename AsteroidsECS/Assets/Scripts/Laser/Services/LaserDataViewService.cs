using System.Collections.Generic;
using Configs;
using Data;
using UnityEngine;

namespace Laser.Services
{
    public class LaserDataViewService : ILaserDataViewService
    {
        private Dictionary<int, GameObject> _viewsByIds = new();

        public void CreateView(int id, LaserConfig config)
        {
            _viewsByIds[id] = Object.Instantiate(config.ViewPrefab);
        }

        public void SetRotation(int id, float angle)
        {
            _viewsByIds[id].transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        public void SetLength(int id, float length)
        {
            LineRenderer lineRenderer = _viewsByIds[id].GetComponentInChildren<LineRenderer>();

            if (lineRenderer != null)
            {
                lineRenderer.SetPosition(1, new Vector3(0, length));
            }
        }

        public void SetPosition(int id, Point newPosition)
        {
            _viewsByIds[id].transform.position = new Vector3(newPosition.X, newPosition.Y);
        }

        public void Destroy(int id)
        {
            Object.Destroy(_viewsByIds[id]);
            _viewsByIds.Remove(id);
        }

        public void Clear()
        {
            foreach ((int _, GameObject view) in _viewsByIds)
            {
                Object.Destroy(view);
            }
            
            _viewsByIds.Clear();
        }
    }
}