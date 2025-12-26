using Configs;

namespace UI.Services
{
    public interface ILaserDataViewService : IDestroyItemService, IViewPositionService
    {
        void CreateView(int id, LaserConfig config);

        void SetRotation(int id, float angle);

        void SetLength(int id, float length);
    }
}