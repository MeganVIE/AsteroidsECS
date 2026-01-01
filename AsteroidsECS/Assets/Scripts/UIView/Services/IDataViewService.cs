using Configs;

namespace UIView.Services
{
    public interface IDataViewService : IViewPositionService
    {
        void CreateView(int id, EnemyConfig config);
    }
}