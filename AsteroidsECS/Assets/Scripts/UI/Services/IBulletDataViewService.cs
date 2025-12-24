using Configs;

namespace UI.Services
{
    public interface IBulletDataViewService : IDestroyItemService, IViewPositionService
    {
        void CreateView(int id, BulletConfig config);
    }
}