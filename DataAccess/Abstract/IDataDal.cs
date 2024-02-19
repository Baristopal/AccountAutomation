using Entities.Concrete;

namespace DataAccess.Abstract;

public interface IDataDal
{
    Task Insert(DataModel model);
    Task<IEnumerable<DataModel>> GetAll();
    Task Update(DataModel model);
    Task<DataModel> GetById(string id);
    Task<IEnumerable<CaseModel>> GetCase();
}
