using Utils;

namespace UI
{
    public interface IEntityDataViewService
    {
        void CreateView(int id, EntityConfig config);
        void SetPosition(int id, Point newPosition);
    }
}