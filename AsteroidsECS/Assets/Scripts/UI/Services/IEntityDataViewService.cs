using Configs;
using Data;
using Utils;

namespace UI.Services
{
    public interface IEntityDataViewService
    {
        void CreateView(int id, AsteroidConfig config);
        void SetPosition(int id, Point newPosition);
    }
}