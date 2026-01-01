using Configs;
using UIView.Services;

namespace Laser.Services
{
    public interface ILaserDataViewService : IDestroyItemService, IViewPositionService, IViewClearService
    {
        void CreateView(int id, LaserConfig config);

        void SetRotation(int id, float angle);

        void SetLength(int id, float length);
    }
}