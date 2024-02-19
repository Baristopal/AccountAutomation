using Entities.Concrete;

namespace DataAccess.Abstract;

public interface IPersonnelDal
{
    Task InsertAsync(PersonnelModel model);
    Task UpdateAsync(PersonnelModel model);
    Task<IEnumerable<PersonnelModel>> GetAllAsync();
    Task<PersonnelModel> GetByIdAsync(string id);
    Task DeleteByIdAsync(string id);

}
