using Configs;
using Utils;

namespace UI.Services
{
    public interface IShipDataViewService
    {
        void CreateView(ShipConfig config);
        void SetPosition(Point newPosition);
        void SetShipRotation(float angle);
    }
}