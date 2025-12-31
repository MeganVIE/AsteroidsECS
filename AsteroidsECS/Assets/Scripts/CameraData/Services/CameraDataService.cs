using Data;
using UnityEngine;

namespace CameraData.Services
{
    public class CameraDataService : ICameraDataService
    {
        private readonly float _halfViewportHeight;
        private readonly float _halfViewportWidth;

        public CameraDataService()
        {
            var main = Camera.main;
            
            if (main != null)
            {
                _halfViewportHeight = main.orthographicSize;
                _halfViewportWidth = _halfViewportHeight * main.aspect;
            }
        }

        public Point GetHalfViewport()
        {
            return new Point(_halfViewportWidth, _halfViewportHeight);
        }
    }
}