using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Models;

namespace Business.Concrete;
public class DefinationManager : IDefinationService
{
    private readonly IDefinationDal _definationDal;

    public DefinationManager(IDefinationDal definationDal)
    {
        _definationDal = definationDal;
    }

    public async Task<BaseResponse<T>> Add<T>(string documentId, T model)
    {
        await _definationDal.InsertAsync<T>(documentId, model);
        return new BaseResponse<T>(model, true);
    }

    public async Task<BaseResponse<T>> Update<T>(string documentId, T model)
    {
        await _definationDal.UpdateAsync<T>(documentId, model);
        return new BaseResponse<T>(model, true);
    }

    public async Task<BaseResponse<IEnumerable<T>>> GetAll<T>()
    {
        var result = await _definationDal.GetAllAsync<T>();
        return new BaseResponse<IEnumerable<T>>(result, true);
    }

    public async Task<BaseResponse<T>> GetById<T>(string id)
    {
        var result = await _definationDal.GetByIdAsync<T>(id);
        return new BaseResponse<T>(result, true);
    }

}
