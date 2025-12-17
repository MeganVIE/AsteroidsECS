using Configs;
using Data;

namespace UI.Services
{
    public interface IShipDataViewService
    {
        void CreateView(ShipConfig config);
        void SetPosition(Point newPosition);
        void SetShipRotation(float angle);
        void Destroy();
    }
}