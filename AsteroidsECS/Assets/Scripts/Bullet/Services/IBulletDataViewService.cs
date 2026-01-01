using Configs;
using UIView.Services;

namespace Bullet.Services
{
    public interface IBulletDataViewService : IDestroyItemService, IViewPositionService, IViewClearService
    {
        void CreateView(int id, BulletConfig config);
    }
}