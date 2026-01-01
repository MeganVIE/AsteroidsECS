using Configs;

namespace UI.Services
{
    public interface IDataViewService : IViewPositionService
    {
        void CreateView(int id, EnemyConfig config);
    }
}