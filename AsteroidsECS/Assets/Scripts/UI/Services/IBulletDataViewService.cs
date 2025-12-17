using Configs;
using Data;

namespace UI.Services
{
    public interface IBulletDataViewService
    {
        void CreateView(int id, BulletConfig config);
        void SetPosition(int id, Point newPosition);
        void Destroy(int id);
    }
}