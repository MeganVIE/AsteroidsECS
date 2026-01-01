using Configs;
using UI.Services;

namespace Bullet.Services
{
    public interface IBulletDataViewService : IDestroyItemService, IViewPositionService, IViewClearService
    {
        void CreateView(int id, BulletConfig config);
    }
}