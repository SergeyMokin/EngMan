using System.Linq;

namespace EngMan.Repository
{
    public interface IRepository<ReturnType, ParameterType>
    {
        IQueryable<ReturnType> GetAll();

        ReturnType Get(int id);

        bool Add(ParameterType value);

        bool Edit(ParameterType value);

        int Delete(int id);

        IQueryable<string> GetAllCategories();

        IQueryable<ReturnType> GetByCategory(string category);
    }
}