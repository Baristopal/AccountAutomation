using Entities.Concrete;

namespace DataAccess.Abstract;

public interface ICheckDal
{
    Task Add(CheckModel model);
    Task Update(CheckModel model);
    Task<IEnumerable<CheckModel>> GetAll();
    Task<CheckModel> GetById(string id);
}
