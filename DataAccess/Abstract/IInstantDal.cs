using Entities.Concrete;

namespace DataAccess.Abstract;

public interface IInstantDal
{
    Task Add(InstantModel model);
    Task Update(InstantModel model);
    Task<IEnumerable<InstantModel>> GetAll();
    Task<InstantModel> GetById(string id);
}
