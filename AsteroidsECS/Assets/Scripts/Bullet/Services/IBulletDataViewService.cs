using Configs;
using UI.Services;

namespace Bullet.Services
{
    public interface IBulletDataViewService : IDestroyItemService, IViewPositionService
    {
        void CreateView(int id, BulletConfig config);
    }
}