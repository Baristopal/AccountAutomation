using Entities.Concrete;

namespace DataAccess.Abstract;

public interface IDefinationDal
{
    Task InsertAsync<T>(string documentId, T model);
    Task UpdateAsync<T>(string documentId, T model);
    Task<IEnumerable<T>> GetAllAsync<T>();
    Task<T> GetByIdAsync<T>(string id);
}
