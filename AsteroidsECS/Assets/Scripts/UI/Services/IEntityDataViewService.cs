using Configs;
using Utils;

namespace UI.Services
{
    public interface IEntityDataViewService
    {
        void CreateView(int id, EntityConfig config);
        void SetPosition(int id, Point newPosition);
    }
}