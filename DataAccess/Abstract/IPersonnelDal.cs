using Entities.Concrete;

namespace DataAccess.Abstract;

public interface IPersonnelDal
{
    Task<IEnumerable<PersonnelModel>> GetAll();
    Task<PersonnelModel> GetById(string id);
    Task Insert(PersonnelModel model);
    Task Update(PersonnelModel model);
}
