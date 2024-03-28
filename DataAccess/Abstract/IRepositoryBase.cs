namespace DataAccess.Abstract;
public interface IRepositoryBase
{
    Task<int> DeleteByIdAsync<T>(int id, int companyId);
    Task<IEnumerable<T>> GetAllAsync<T>(int companyId);
    Task<T> GetByIdAsync<T>(string id);
    Task<int> InsertAsync<T>(T t);
    Task<int> UpdateAsync<T>(T t);
}
